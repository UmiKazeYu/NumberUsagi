using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockGenerator : MonoBehaviour
{
    public GameObject block;
    public GameObject numberText;
    public GameObject canvas;
    int start = 1;
    int end = 10;
    float interval = 1.4f;
    Vector2 blockPosition = new Vector2(-6.0f, -4.5f);
    Vector2 textPosition = new Vector2(230, 33);

    List<int> numbers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        while (numbers.Count > 0)
        {
            int index = Random.Range(0, numbers.Count);

            int random = numbers[index];

            blockPosition.x += interval;

            GameObject myBlock = Instantiate(block, blockPosition, Quaternion.identity);
            GameObject myCanvas = Instantiate(canvas, this.transform.position, Quaternion.identity, myBlock.transform);
            GameObject myText = Instantiate(numberText, textPosition, Quaternion.identity, myCanvas.transform);

            myText.GetComponent<Text>().text = random.ToString();

            myBlock.gameObject.name = "Block" + random;

            textPosition.x += 75.5f;

            numbers.RemoveAt(index);
        }

        GameObject firstBlock = GameObject.Find("Block1");
        firstBlock.tag = "NextBlockTag";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
