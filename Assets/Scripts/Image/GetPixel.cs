using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPixel : MonoBehaviour {
  [SerializeField]
  private GameObject pixelCube;
  [SerializeField]
  private GameObject image;
  void Start () {
    // テクスチャを取得
    Texture2D texture = (Texture2D)image.GetComponent<Renderer>().material.mainTexture;
    // テクスチャのピクセルカラーを取得
    Color[] texturePixels = texture.GetPixels();
    // 出力先の親オブジェクトの位置
    Vector3 outputPosition = this.transform.position;
    // 出力するcubeのスケール Planeに対するピクセルの比率に合わせる
    // float cubeScale = pixelCube.transform.localScale.x;
    float cubeScale = image.transform.localScale.x * 10 / texture.width;
    // cubeの出力位置
    Vector3 pixelPosition;
    // 出力したcubeオブジェクト
    GameObject pixel;
    for (int y = 0; y < texture.height; y++) {
      for (int x = 0; x < texture.width; x++) {
        // cubeの出力位置を決定 親オブジェクトの位置を基準にcubeのスケールずつずらす
        pixelPosition = new Vector3(outputPosition.x + x * cubeScale, outputPosition.y - y * cubeScale, outputPosition.z);
        // cubeの出力
        pixel = Instantiate(pixelCube, pixelPosition, Quaternion.identity, this.transform);
        // 出力したcubeの色を、対応するテクスチャのピクセルカラーに変更
        pixel.GetComponent<Renderer>().material.color = texturePixels[x + texture.width * y];
        // 出力したcubeのスケールを変更
				pixel.transform.localScale = Vector3.one * cubeScale;
      }
    }
    // 出力結果が上下逆になってるので反転
    this.transform.Rotate(180f, 0f, 0f);
  }
}
