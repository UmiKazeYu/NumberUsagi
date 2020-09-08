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
    float offsetX = -6.0f;
    float offsetY = -4.5f;
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
            Debug.Log(random);

            blockPosition.x += interval;

            GameObject thisBlock = Instantiate(block, blockPosition, Quaternion.identity);
            GameObject thisCanvas = Instantiate(canvas, this.transform.position, Quaternion.identity, thisBlock.transform);
            GameObject thisText = Instantiate(numberText, textPosition, Quaternion.identity, thisCanvas.transform);
            thisText.GetComponent<Text>().text = random.ToString();

            thisBlock.gameObject.name = random.ToString();

            textPosition.x += 75.5f;

            numbers.RemoveAt(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
