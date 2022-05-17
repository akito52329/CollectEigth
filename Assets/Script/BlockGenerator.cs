using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] GameObject parentObj;
    [SerializeField] GameObject[] blockGenePos;
    [SerializeField] Color[] colorDate;
    [SerializeField] List<int> generationNumber;
    [SerializeField] int min;
    [SerializeField] float stopTime = 0.01f;
    public GameObject blockPrefab;
    public Action OnCollisionAction = null;

    public string destinationTag = "Block";

    public int GenerationNumber()//生成する個数
    {
        return Random.Range(min, blockGenePos.Length);
    }

    public void GenerationPos()//生成場所
    {
        int i = 0;
        int geneCount = GenerationNumber();//生成個数

        while (i <= geneCount)
        {
            if (geneCount != blockGenePos.Length)
            {
                var pos = Random.Range(0, blockGenePos.Length);//生成位置を決める
                if (!generationNumber.Contains(pos))//重複していないかをみる
                {
                    generationNumber.Add(pos);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                generationNumber.Add(i);//全ポジション生成
            }
            i++;
        }
        Generation();
    }

    Color GetColor()//ランダムで色を渡す
    {
        var num = Random.Range(0, colorDate.Length);
        return colorDate[num];
    }

    public void Generation()
    {
        StartCoroutine("BlockCount");
    }

    IEnumerator BlockCount()//生成
    {
        var count = generationNumber.Count;
        while (count-- > 0)
        {
            yield return new WaitForSeconds(stopTime);
            var parent = parentObj.transform;
            var blockObject = Instantiate(blockPrefab, blockGenePos[generationNumber[count]].transform.position, transform.rotation, parent);
            Block block = blockObject.GetComponent<Block>();
            block.ColorChenge(GetColor());
        }
        generationNumber.Clear();
    }
}