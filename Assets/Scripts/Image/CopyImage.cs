using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CopyImage : MonoBehaviour
{
  [SerializeField]
  private Mesh pixelMesh;          // ピクセルを表現するメッシュ
  [SerializeField]
  private Material pixelMaterial;  // ピクセルのマテリアル（描画用シェーダー）
  [SerializeField]
  private ShadowCastingMode castShadows = ShadowCastingMode.Off;
  [SerializeField]
  private bool receiveShadows = false;
  [SerializeField]
  private ComputeShader positionComputeShader;  // コンピュートシェーダー
  [SerializeField]
  private GameObject image;                     // テクスチャを貼ってるオブジェクト

  private int pixelCount;               // ピクセル数
  private int positionComputeKernelId;  // カーネルID
  private ComputeBuffer positionBuffer; // ピクセル位置を格納するバッファ
  private ComputeBuffer colorBuffer;    // ピクセルカラーを格納するバッファ
  private ComputeBuffer argsBuffer;     // DrawMeshInstancedIndirect用のバッファ
  private uint[] args = new uint[] { 0, 0, 0, 0, 0 };  // DrawMeshInstancedIndirect用の配列

  void Start()
  {
    CreateBuffers();
  }

  void Update()
  {
    UpdateBuffers();
    Graphics.DrawMeshInstancedIndirect(pixelMesh, 0, pixelMaterial, pixelMesh.bounds, argsBuffer, 0, null, castShadows, receiveShadows);
  }

  private void CreateBuffers()
  {
    // テクスチャ画像の取得
    Texture2D texture = (Texture2D)image.GetComponent<Renderer>().material.mainTexture;
    // ピクセル数の計算
    int dim = texture.width;
    pixelCount = dim * dim;

    // カーネルIDの取得
    positionComputeKernelId = positionComputeShader.FindKernel("CSMain");

    // バッファの初期化
    argsBuffer = new ComputeBuffer(5, sizeof(uint), ComputeBufferType.IndirectArguments);
    positionBuffer = new ComputeBuffer(pixelCount, 16);
    colorBuffer = new ComputeBuffer(pixelCount, 16);
    
    // テクスチャのピクセルカラーをcolorBufferにセット
    colorBuffer.SetData(texture.GetPixels());

    // 描画用シェーダーにバッファをセット
    pixelMaterial.SetBuffer("positionBuffer", positionBuffer);
    pixelMaterial.SetBuffer("colorBuffer", colorBuffer);

    // pixelMesh.bounds = new Bounds(this.transform.position, Vector3.one * 10000f);

    // argsBufferにメッシュの頂点数とメッシュの数を格納
    uint numIndices = (pixelMesh != null) ? (uint)pixelMesh.GetIndexCount(0) : 0;
    args[0] = numIndices;
    args[1] = (uint)pixelCount;
    argsBuffer.SetData(args);

    // コンピュートシェーダーにバッファ、値をセット
    positionComputeShader.SetBuffer(positionComputeKernelId, "positionBuffer", positionBuffer);
    positionComputeShader.SetFloat("_Dim", dim);
    float pixelScale = image.transform.localScale.x * 10 / dim;
    positionComputeShader.SetFloat("_PixelScale", pixelScale);
    positionComputeShader.SetVector("_Pivot", this.transform.position);
  }

  private void UpdateBuffers()
  {
    // カーネルの実行
    positionComputeShader.Dispatch(positionComputeKernelId, pixelCount / 8, 1, 1);
  }

  void OnDisable()
  {
    // バッファの解放
    if (positionBuffer != null) positionBuffer.Release();
    positionBuffer = null;
    if (colorBuffer != null) colorBuffer.Release();
    colorBuffer = null;
    if (argsBuffer != null) argsBuffer.Release();
    argsBuffer = null;
  }

  // void OnGUI()
  // {
  //   GUI.Label(new Rect(265, 12, 200, 30), "Instance Count: " + pixelCount.ToString("N0"));
  // }
}
