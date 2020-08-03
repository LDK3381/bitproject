using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEvent : MonoBehaviour
{
    public float cubeSize = 0.1f;
    public int cubesInRow = 2;

    private float cubesPivotDistance = 0f;
    private Vector3 cubesPivot;

    public float explosionForce  = 5f;
    public float explosionRadius = 1f;
    public float explosionUpward = 0.1f;

    void Start()
    {
        try
        {
            //calculate pivot distance
            cubesPivotDistance = cubeSize * cubesInRow / 2;
            //use this value to create pivot vector)
            cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        }
        catch
        {
            Debug.Log("WallEvent.Start Error");
        }
    }

    public void Explode()
    {
        try
        {
            //벽 파괴 이벤트
            gameObject.SetActive(false);

            Piece();

            //벽 파괴 위치 지점
            Vector3 explosionPos = transform.position;
            //해당 지점에서 Collider 가져오기
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
            //벽 파괴로 인한 파편 이동효과 구현
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
                }
            }
        }
        catch
        {
            Debug.Log("WallEvent.Explode Error");
        }
    }

    private void Piece()
    {
        try
        {
            //5x5x5 크기의 파편 생성
            for (int x = 0; x < cubesInRow; x++)
            {
                for (int y = 0; y < cubesInRow; y++)
                {
                    for (int z = 0; z < cubesInRow; z++)
                    {
                        CreatePiece(x, y, z);
                    }
                }
            }
        }
        catch
        {
            Debug.Log("WallEvent.Piece Error");
        }
    }

    private void CreatePiece(int x, int y, int z)
    {
        try
        {
            //박살난 벽 파편 오브젝트 선언 및 생성
            GameObject piece;
            piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //파편 오브젝트의 텍스쳐를 Wood로 적용
            piece.GetComponent<Renderer>().material.mainTexture = Resources.Load("Block_WoodUV") as Texture;

            //벽 파편 생성 위치
            piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
            piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

            //충돌처리
            piece.AddComponent<Rigidbody>();
            piece.GetComponent<Rigidbody>().mass = cubeSize;

            //2초 후에 벽 파편 삭제
            Destroy(piece, 1);
        }
        catch
        {
            Debug.Log("WallEvent.CreatePiece Error");
        }
    }
}
