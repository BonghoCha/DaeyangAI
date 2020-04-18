using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class url_debug : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
       
    }

    void update()
    {

    }

    public void FAQ_button_on(int num)
    {
        if (num == 0)
        {
            Debug.Log("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=697");
        }

        if (num == 1)
        {
            Debug.Log("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=698");
        }

        if (num == 2)
        {
            Debug.Log("http://board.sejong.ac.kr/pages/faq.html");
        }

        if (num == 3)
        {
            Debug.Log("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=353");
        }
    }





}
