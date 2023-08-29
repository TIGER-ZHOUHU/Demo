using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject player;

    [Header("Transform")]
    private float m_speed = 5f;
    private float m_rotate = 150f;
 
    void Update()
    {
        //键盘控制移动
        PlayerMove_KeyTransform();
    }

    private void PlayerRotate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePostion =
            Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.y));
        
    }
    
    //通过Transform组件 键盘控制移动
    private void PlayerMove_KeyTransform()
    {
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)) //前
        {
            player.transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
        {
            player.transform.Translate(Vector3.forward * -m_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
        {
            player.transform.Translate(Vector3.right * -m_speed * Time.deltaTime);
            //player.transform.Rotate(0,-m_rotate*Time.deltaTime,0);
        }
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
        {
            player.transform.Translate(Vector3.right * m_speed * Time.deltaTime);
            //player.transform.Rotate(0,m_rotate*Time.deltaTime,0);
        }
    }
}
