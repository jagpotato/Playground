using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CopyImage : MonoBehaviour
{
  // [SerializeField]
  private int instanceCount;  // インスタンス数
  [SerializeField]
  private Mesh instanceMesh;  // インスタンスにするメッシュ
  [SerializeField]
  private Material instanceMaterial;  // インスタンスのマテリアル
  [SerializeField]
  private ShadowCastingMode castShadows = ShadowCastingMode.Off;
  [SerializeField]
  private bool receiveShadows = false;
  [SerializeField]
  private ComputeShader positionComputeShader;  // コンピュートシェーダー
  [SerializeField]
  private GameObject image;

  private int positionComputeKernelId;  // カーネルID
  private ComputeBuffer positionBuffer;
  private ComputeBuffer argsBuffer;
  private ComputeBuffer colorBuffer;
  private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

  void Start()
  {
    argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
    CreateBuffers();
  }

  void Update()
  {
    UpdateBuffers();
    Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, instanceMesh.bounds, argsBuffer, 0, null, castShadows, receiveShadows);
  }

  private void CreateBuffers()
  {
    // インスタンスは最低1個
    // if (instanceCount < 1) instanceCount = 1;
    // インスタンス数を2のべき乗に
    // instanceCount = Mathf.ClosestPowerOfTwo(instanceCount);
    
    //
    Texture2D texture = (Texture2D)image.GetComponent<Renderer>().material.mainTexture;
    int dim = texture.width;
    instanceCount = dim * dim;

    // カーネルIDの取得
    positionComputeKernelId = positionComputeShader.FindKernel("CSMain");
    instanceMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

    if (positionBuffer != null) positionBuffer.Release();
    if (colorBuffer != null) colorBuffer.Release();

    // バッファの初期化
    positionBuffer = new ComputeBuffer(instanceCount, 16);
    colorBuffer = new ComputeBuffer(instanceCount, 16);
    // テクスチャのピクセルカラーをcolorBufferにセット
    colorBuffer.SetData(texture.GetPixels());

    // 描画用シェーダーにバッファをセット
    instanceMaterial.SetBuffer("positionBuffer", positionBuffer);
    instanceMaterial.SetBuffer("colorBuffer", colorBuffer);

    uint numIndices = (instanceMesh != null) ? (uint)instanceMesh.GetIndexCount(0) : 0;
    args[0] = numIndices;
    args[1] = (uint)instanceCount;
    argsBuffer.SetData(args);

    // コンピュートシェーダーにバッファ、値をセット
    positionComputeShader.SetBuffer(positionComputeKernelId, "positionBuffer", positionBuffer);
    positionComputeShader.SetFloat("_Dim", dim);
    float cubeScale = image.transform.localScale.x * 10 / dim;
		positionComputeShader.SetFloat("_CubeScale", cubeScale);
    positionComputeShader.SetVector("_Pivot", this.transform.position);
  }

  private void UpdateBuffers()
  {
    positionComputeShader.Dispatch(positionComputeKernelId, instanceCount, 1, 1);
  }

  void OnDisable()
  {
    if (positionBuffer != null) positionBuffer.Release();
    positionBuffer = null;
    if (colorBuffer != null) colorBuffer.Release();
    colorBuffer = null;
    if (argsBuffer != null) argsBuffer.Release();
    argsBuffer = null;
  }

  void OnGUI()
  {
    GUI.Label(new Rect(265, 12, 200, 30), "Instance Count: " + instanceCount.ToString("N0"));
  }
}
