using UnityEngine;
using UnityEngine.UI;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using AssetPackage;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    bool is_start = false;
    bool is_checked = false;
    bool is_waiting = false;
    bool is_print = false;
    bool ca_app = false;
    bool ca_button = false;

    int neg = 0;
    int hap = 0;
    int neu = 0;
    int sur = 0;

    int findmax = 0;

    string findemo = "";

    public GameObject em_t;
    public GameObject em_s;
    public GameObject btns;
    public GameObject ca_intro;
    public GameObject picture;
    public GameObject count;

    //string[] lines = new string[10];
    //int cnt = 0;

    public IEnumerator CheckTime()
    {
        Debug.Log("시작");
        is_waiting = true;

        yield return new WaitForSeconds(3.0f);
        is_print = true;
        is_checked = false;
        is_start = true;
        ca_button = false;
        is_waiting = false;
        Debug.Log("끝");
        neg = 0;
        hap = 0;
        neu = 0;
        sur = 0;
        findmax = 0;

        if (ca_app == true && em_s.GetComponent<Text>().text != "")
        {
            Go_Picture();
        }

        /*
        if(em_s.GetComponent<Text>().text == "")
        {
            em_t.GetComponent<Text>().text = "Sad";
            em_s.GetComponent<Text>().text = "얼굴이 제대로 인식되지 않아서 슬퍼요ㅜㅜ";
        }

        */

        //webcam.Pause();
    }
    //! http://answers.unity3d.com/questions/909967/getting-a-web-cam-to-play-on-ui-texture-image.html

    /// <summary>
    /// The rawimage, used to show webcam output.  
    /// </summary>
    ///
    /// <remarks>
    /// In this demo, rawimage is the Canvas of the scene.
    /// </remarks>
    public RawImage rawimage;

    /// <summary>
    /// The emotions, used to show the detedted emotions.
    /// </summary>
    ///
    /// <remarks>
    /// In this demo, emotions is the bottom Text object of the scene.
    /// </remarks>
    public Text emotions;

    /// <summary>
    /// The message, used to signal the number of faces detected.
    /// </summary>
    /// 
    /// <remarks>
    /// In this demo, emotions is the top Text object of the scene.
    /// </remarks>
    public Text msg;

    //! https://answers.unity3d.com/questions/1101792/how-to-post-process-a-webcamtexture-in-realtime.html

    /// <summary>
    /// The webcam.
    /// </summary>
    WebCamTexture webcam;

    /// <summary>
    /// The output.
    /// </summary>
    Texture2D output;

    /// <summary>
    /// The data.
    /// </summary>
    Color32[] data;

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        emo_txt_read();

        em_t = GameObject.Find("EM_Text");
        em_s = GameObject.Find("EM_St");
        btns = GameObject.Find("Bottom_btns");
        picture = GameObject.Find("Picture_btns");



        /*
        string date = DateTime.Now.ToString("yyyy_MM_dd_HH");
        Debug.Log(date);
        if (date == "2020_02_11_20")
        {
            Time.timeScale = 0;
        }
        */
        //1) Enumerate webcams
        //
        WebCamDevice[] devices = WebCamTexture.devices;

        //2) for debugging purposes, prints available devices to the console
        //
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //! http://answers.unity3d.com/questions/909967/getting-a-web-cam-to-play-on-ui-texture-image.html
        //WebCamTexture webcam = new WebCamTexture();
        //rawimage.texture = webcam;
        //rawimage.material.mainTexture = webcam;
        //webcamTexture.Play();

        //! https://answers.unity3d.com/questions/1101792/how-to-post-process-a-webcamtexture-in-realtime.html
        //3) Create a WebCamTexture (size should not be to big)
        webcam = new WebCamTexture(640, 480); // (Screen.width, Screen.height) 

        //4) Assign the texture to an image in the UI to see output (these two lines are not necceasary if you do 
        //   not want to show the webcam video, but might be handy for debugging purposes)
        rawimage.texture = webcam;
        rawimage.material.mainTexture = webcam;

        //5) Start capturing the webcam.
        //
        webcam.Play();

        //6) ??
        //output = new Texture2D(webcam.width, webcam.height);
        //GetComponent<Renderer>().material.mainTexture = output;

        // 7) Create an array to hold the ARGB data of a webcam video frame texture. 
        //
        data = new Color32[webcam.width * webcam.height];

        //8) Create an EmotionDetectionAsset
        //
        //   The asset will load the appropriate dlibwrapper depending on process and OS.
        //   Note that during development unity tends to use the 32 bits version where 
        //   during playing it uses either 32 or 64 bits version dependend on the OS.
        //   
        eda = new EmotionDetectionAsset();

        //9) Assign a bridge (no interfaces are required but ILog is convenient during development.
        // 
        eda.Bridge = new dlib_csharp.Bridge();

        //10) Init the EmotionDetectionAsset. 
        //    Note this takes a couple of seconds as it need to read/parse the shape_predictor_68_face_landmarks database
        // 
        eda.Initialize(@"Assets\", database);

        //11) Read the fuzzy logic rules and parse them.
        // 
        String[] lines = File.ReadAllLines(furia);
        eda.ParseRules(lines);

        Debug.Log("Emotion detection Ready for Use");
    }

    Int32 frames = 0;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        //end = false;
        //Invoke("CheckTime", 5.0f);
        // 1) Check if the data array is allocated and we have a created a valid EmotionDetectionAsset.
        //    If so, process every 5th frame.
        // 
        if (data != null && eda != null && (++frames) % 5 > 0)
        {
            // 2) Get the raw 32 bits ARGB data from the frame.
            // 
            webcam.GetPixels32(data);

            // 3) Process this ARGB Data.
            // 
            ProcessColor32(data, webcam.width, webcam.height);

            // You can play around with data however you want here.
            // Color32 has member variables of a, r, g, and b. You can read and write them however you want.

            //output.SetPixels32(data);
            //output.Apply();

            // For debugging it might be handy to stop processsing after a number of processed frames.
            // 
            //if (frames == 0)
            //{
            // webcam.Stop();
            //}

            frames = 0;
        }
    }

    /// <summary>
    /// A face (test input).
    /// </summary>
    const String face3 = @"Assets\Kiavash1.jpg";

    /// <summary>
    /// The Furia Fuzzy Logic Rules.
    /// </summary>
    const String furia = @"Assets\FURIA Fuzzy Logic Rules.txt";

    /// <summary>
    /// The landmark database.
    /// </summary>
    const String database = @"Assets\shape_predictor_68_face_landmarks.dat";

    /// <summary>
    /// http://ericeastwood.com/blog/17/unity-and-dlls-c-managed-and-c-unmanaged
    /// https://docs.unity3d.com/Manual/NativePluginInterface.html.
    /// </summary>
    EmotionDetectionAsset eda;

    /// <summary>
    /// Loads a PNG.
    /// </summary>
    ///
    /// <param name="filePath"> Full pathname of the file. </param>
    ///
    /// <returns>
    /// The PNG.
    /// </returns>
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(640, 864, TextureFormat.RGBA32, false); //(texture.width, texture.height, TextureFormat.RGB24, false)
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions. 텍스쳐 크기가 자동 조정.
        }
        return tex;
    }

    /// <summary>
    /// Color32 array to byte array.
    /// </summary>
    ///
    /// <param name="colors"> The colors. </param>
    ///
    /// <returns>
    /// A byte[].
    /// </returns>
    private static byte[] Color32ArrayToByteArray(UnityEngine.Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
            return null;

        int lengthOfColor32 = Marshal.SizeOf(typeof(UnityEngine.Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
                handle.Free();
        }

        return bytes;
    }

    /// <summary>
    /// Executes the click action.
    /// This will process face3.
    /// </summary>
    public void onClick()
    {
        // How to detect emotions in a Texture.
        // 
        //onClickTexture();

        // How to detect emotions in a Bitmap Texture
        // 
        onClickBitmap();

        // How to detect emotions in an Image.
        // This will need a using System.Drawing; statemnt and System.Drawing.dll from Mono being dropped into the asset folder.
        // Unity does not support .Net Images without this workaround.
        // 
        //onClickImage();
    }

    /// <summary>
    /// Executes the click texture action.
    /// 
    /// Example of how to process a texture.
    /// </summary>
    ///
    /// <remarks>
    /// Demo code.
    /// </remarks>
    public void onClickTexture()
    {
        //! Save both spike detection and averaging.
        //
        Int32 avg = (eda.Settings as EmotionDetectionAssetSettings).Average;
        Boolean spike = (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes;

        Texture2D texture = LoadPNG(face3);

        // Reference (0,0) is BottomLeft !! instead of Topleft.
        // 
        ProcessTexture(texture);

        //! Save both spike detection and averaging.
        //
        (eda.Settings as EmotionDetectionAssetSettings).Average = avg;
        (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes = spike;
    }

    /// <summary>
    /// Executes the click bitmap action.
    /// </summary>
    ///
    /// <remarks>
    /// Demo code.
    /// </remarks>
    public void onClickBitmap()
    {
        // webcam.Stop();

        //! Save both spike detection and averaging.
        //
        Int32 avg = (eda.Settings as EmotionDetectionAssetSettings).Average;
        Boolean spike = (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes;

        System.Drawing.Image img = System.Drawing.Image.FromFile(@"Assets\dump.bmp");

        ProcessImage(img);

        //! Save both spike detection and averaging.
        //
        (eda.Settings as EmotionDetectionAssetSettings).Average = avg;
        (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes = spike;
    }

    /// <summary>
    /// Process the texture described by texture.
    /// </summary>
    ///
    /// <remarks>
    /// Demo code.
    /// </remarks>
    ///
    /// <param name="texture"> The texture. </param>
    private void ProcessTexture(Texture2D texture)
    {
        //UnityEngine.Color c = texture.GetPixel(0, texture.height - 1);

        //// ARGB[255] 253 216 174 (texture bottomleft)
        //// ARGB[255] 229 198 154 (topleft)
        //Debug.LogWarning(String.Format("ARGB [{0:0}] {1:0} {2:0} {3:0}",
        //    c.a * 255,
        //    c.r * 255,
        //    c.g * 255,
        //    c.b * 255));

        Rect r = new Rect(0, 0, texture.width, texture.height);

        Sprite sprite = Sprite.Create(texture, r, Vector2.zero);

        GetComponent<UnityEngine.UI.Image>().sprite = sprite;

        //UnityEngine.UI.Image ui = (UnityEngine.UI.Image)(GameObject.Find("anmimage"));
        //ui.sprite = sprite;

        //Material m = new Material(M
        //animage.maintexture = texture;

        //GUI.DrawTexture(,
        //    texture);

        ProcessColor32(texture.GetPixels32(), texture.width, texture.height);
    }




    /// <summary>
    /// Process the color 32.
    /// </summary>
    ///
    /// <remarks>
    /// This method is used to process raw data from Unity webcam frame textures.
    /// </remarks>
    ///
    /// <param name="pixels"> The pixels. </param>
    /// <param name="width">  The width. </param>
    /// <param name="height"> The height. </param>
    /// 
    private void ProcessColor32(Color32[] pixels, Int32 width, Int32 height)
    {

        //RectTransform face = GameObject.Find("T_Image").GetComponent<RectTransform>();

        // Convert raw ARGB data into a byte array.
        // 
        byte[] raw = Color32ArrayToByteArray(pixels);

        // Disable Average and SpikeSupression. Needed only for single unrelated images  
        // For video of the same person, adjst this to your need (or disable both lines for default
        // settings) . 
        (eda.Settings as EmotionDetectionAssetSettings).Average = 1;
        (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes = false;


        // Load image into detection 
        // 
        // and  
        // 
        // Try to detect faces. This is the most time consuming part.
        // 
        // Note there the formats supported are limited to 24 and 32 bits RGB at the moment.
        // 
        if (eda.ProcessImage(raw, width, height, true))
        {
            msg.text = String.Format("{0} Face(s detected.", eda.Faces.Count);

            // Process each detected face by detecting the 68 landmarks in each face
            // 
            if (eda.ProcessFaces())
            {
                // Process landmarks into emotions using fuzzy logic.
                // 
                is_checked = true;
                if (eda.ProcessLandmarks())
                {
                    // Extract results.
                    // 
                    Dictionary<string, double> emos = new Dictionary<string, double>();

                    foreach (String emo in eda.Emotions)
                    {
                        // Debug.LogFormat("{0} scores {1}.", emo, eda[0, emo]);

                        // Extract (averaged) emotions of the first face only.
                        // 

                        emos[emo] = eda[0, emo];


                        //if ((emos[emo] >= 0.86 && ca_app == false) || (emos[emo] >= 0.86 && ca_app == true && ca_button == true))
                        if ( (ca_app == false) || (ca_app == true && ca_button == true))
                        {
                            if (!is_waiting)
                            {
                                StartCoroutine("CheckTime");
                            }

                            if (is_waiting )
                            {
                                if (emos[emo] >= 0.86)
                                {

                                    Debug.Log(emo);

                                    if (emo == "Anger" || emo == "Fear" || emo == "Disgust" || emo == "Sad")
                                    {
                                        neg++;
                                        if (findmax < neg)
                                        {
                                            findmax = neg;
                                            findemo = "Negative";
                                        }
                                    }
                                    if (emo == "Happy")
                                    {
                                        hap++;
                                        if (findmax < hap)
                                        {
                                            findmax = hap;
                                            findemo = emo;
                                        }
                                    }
                                    if (emo == "Neutral")
                                    {
                                        neu++;
                                        if (findmax < neu)
                                        {
                                            findmax = neu;
                                            findemo = emo;
                                        }
                                    }


                                    if (emo == "Surprise")
                                    {
                                        sur++;
                                        if (findmax < sur)
                                        {
                                            findmax = sur;
                                            findemo = emo;
                                        }
                                    }
                                }
                                is_start = false;
                            }

                        }

                        if (is_print)
                        {
                            EmoionStationery(findemo);
                            //GameObject.Find("Emo").GetComponent<Text>().text = emo;

                            string path = @"Assets\Emo_Saving\"; ;

                            string datefile = DateTime.Now.ToString("yyyy_MM_dd");

                            File.AppendAllText(path + datefile + "_emofile.txt", findemo + "\r\n");

                            is_print = false;
                        }
                    }


                    //foreach (var temp in eda.Faces)
                    //{
                    //    Debug.Log(temp.Key);

                    //    face.offsetMax = new Vector2(temp.Key.Right, temp.Key.Bottom);
                    //    face.offsetMin = new Vector2(temp.Key.Left, temp.Key.Top);

                    //    if ((temp.Key.Bottom - temp.Key.Top) * (temp.Key.Right - temp.Key.Left)>80000)
                    //    {
                    //        webcam.Stop();
                    //    }
                    //}


                    //Create the emotion strings.
                    // 
                    emotions.text = String.Join("\r\n", emos.OrderBy(p => p.Key).Select(p => String.Format("{0}={1:0.00}", p.Key, p.Value)).ToArray());
                    //print("msg: " + emotions.text);
                    //emotions.text = Log result in Console
                    is_checked = false;
                }
                else
                {
                    emotions.text = "No emotions detected";
                }
            }
            else
            {
                emotions.text = "No landmarks detected";
            }
        }
        else
        {
            msg.text = "No Face(s) detected";
            //Debug.Log("얼굴을 인식해주세요");
        }
    }

    /// <summary>
    /// Executes the click image action, processed a System.Drawing.Image.
    /// </summary>
    ///
    /// <remarks>
    /// Demo code.
    /// </remarks>
    public void onClickImage()
    {
        System.Drawing.Image img = System.Drawing.Image.FromFile(face3);

        ProcessImage(img);
    }

    /// <summary>
    /// Process the image described by img.
    /// </summary>
    ///
    /// <param name="img"> The image. </param>
    ///
    /// <remarks>
    /// Demo code.
    /// </remarks>
    private void ProcessImage(System.Drawing.Image img)
    {
        // ARGB[255] 253 216 174 (texture bottomleft)
        // ARGB[255] 229 198 154 (topleft)
        //Debug.LogWarning(String.Format("ARGB [{0:0}] {1:0} {2:0} {3:0}",
        //    ((Bitmap)img).GetPixel(0, 0).A,
        //    ((Bitmap)img).GetPixel(0, 0).R,
        //    ((Bitmap)img).GetPixel(0, 0).G,
        //    ((Bitmap)img).GetPixel(0, 0).B));

        Rect r = new Rect(0, 0, img.Width, img.Height);

        (eda.Settings as EmotionDetectionAssetSettings).Average = 1;
        (eda.Settings as EmotionDetectionAssetSettings).SuppressSpikes = false;

        if (eda.ProcessImage(img))
        {
            msg.text = String.Format("{0} Face(s detected.", eda.Faces.Count);

            if (eda.ProcessFaces())
            {
                if (eda.ProcessLandmarks())
                {
                    Dictionary<string, double> emos = new Dictionary<string, double>();

                    foreach (String emo in eda.Emotions)
                    {
                        //Debug.LogFormat("{0} scores {1}.", emo, eda[0, emo]);
                        emos[emo] = eda[0, emo];
                    }

                    emotions.text = String.Join("\r\n", emos.OrderBy(p => p.Key).Select(p => String.Format("{0}={1:0.00}", p.Key, p.Value)).ToArray());
                }
                else
                {
                    msg.text = "No emotions detected";
                }
            }
            else
            {
                msg.text = "No landmarks detected";
            }
        }
        else
        {
            msg.text = "No Face(s) detected";
        }
    }

        public void emo_txt_read()
        {
            string path = @"Assets\Emo_Saving\";

            string datefile = DateTime.Now.ToString("yyyy_MM_dd") + "_emofile.txt";

        if (!File.Exists(path + datefile))
        {
            File.AppendAllText(path + datefile, findemo + "\r\n");
        }
        string[] textValue = System.IO.File.ReadAllLines(path + datefile);
            if (textValue.Length > 0)
            {
                for (int i = 0; i < textValue.Length; i++)
                {
                    if (textValue[i] == "Negative")
                    {
                        neg++;
                        if (findmax < neg)
                        {
                            findmax = neg;
                            findemo = textValue[i];
                        }
                    }
                    if (textValue[i] == "Happy")
                    {
                        hap++;
                        if (findmax < hap)
                        {
                            findmax = hap;
                            findemo = textValue[i];
                        }
                    }
                    if (textValue[i] == "Neutral")
                    {
                        neu++;
                        if (findmax < neu)
                        {
                            findmax = neu;
                            findemo = textValue[i];
                        }
                    }
                    if (textValue[i] == "Surprise")
                    {
                        sur++;
                        if (findmax < sur)
                        {
                            findmax = sur;
                            findemo = textValue[i];
                        }
                    }
                }
                Debug.Log(findemo + " " + findmax);
            }
        }

    public void EmoionStationery(string em)
    {
        int ran = UnityEngine.Random.Range(0, 4);
        string st = "";

        if (em == "Negative")
        {
            if (ran == 0)
            {
                st = "제가..뭘 잘못했나요..?";
                em_t.GetComponent<Text>().text = "힝..";

                GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(5);
            }
            else if (ran == 1)
            {
                st = "공부가 많이 힘들죠? \n좋은 일이 생길거에요!";
                em_t.GetComponent<Text>().text = "힘내요!";

                GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(7);
            }
            else if (ran == 2)
            {
                st = "저 이상한 인공지능 아니에요..ㅠㅠ";
                em_t.GetComponent<Text>().text = "힝2...";

                GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(3);
            }
            else
            {
                st = "혹시 저한테 화난거 있어요?ㅡㅡ";
                em_t.GetComponent<Text>().text = "우씨!";

                GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(4);
            }
        }
        else if (em == "Happy")
        {
            if (ran == 0)
            {
                st = "좋은 일 있으신 가봐요!";
                em_t.GetComponent<Text>().text = "히히";
            }
            if (ran == 1)
            {
                st = "행복한 기운이 저한테도\n전달되는거 같아요!";
                em_t.GetComponent<Text>().text = "헤헤";
            }
            if (ran == 2 || ran == 3)
            {
                st = "행복한 미소네요~ \n그 미소 잃지 말아요!";
                em_t.GetComponent<Text>().text = "호호";
            }

            GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(1);
        }
        else if (em == "Neutral")
        {
            if (ran == 0)
            {
                st = "활짝 웃어볼까요?";
                em_t.GetComponent<Text>().text = "흠..";
            }
            if (ran == 1)
            {
                st = "표정에 영혼이 없네요..";
                em_t.GetComponent<Text>().text = "에헴..";
            }
            if (ran == 2 || ran == 3)
            {
                st = "난 아무 생각이 없다.\n왜냐하면 아무 생각이 없기 때문이다.";
                em_t.GetComponent<Text>().text = "으흠..";
            }

            GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(6);
        }
        else if (em == "Surprise")
        {
            if (ran == 0)
            {
                st = "입을 크게 벌리면\n얼굴 운동이 된대요!";
                em_t.GetComponent<Text>().text = "오!";
            }
            if (ran == 1)
            {
                st = "왜 이렇게 놀라는 거에요!?\n제가 무섭나요..?";
                em_t.GetComponent<Text>().text = "옹?";
            }
            if (ran == 2 || ran == 3)
            {
                st = "제가 표정을 읽을 수 있다고\n입을 너무 벌리진 말아요...ㅋ";
                em_t.GetComponent<Text>().text = "오옹?";
            }

            GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(2);
        }
        Debug.Log(em);
        Debug.Log(st);
        em_s.GetComponent<Text>().text = st;
    }

    public void Open_App()
    {
        ca_intro.SetActive(true);
        btns.SetActive(true);
        picture.SetActive(false);
        //GameObject.Find("Cam_intro").SetActive(true);
        //GameObject.Find("Bottom_btns").SetActive(true);
        neg = 0;
        hap = 0;
        neu = 0;
        sur = 0;
        findmax = 0;
        findemo = "";
        em_t.GetComponent<Text>().text = "";
        em_s.GetComponent<Text>().text = "";

        //GameObject.Find("EM").SetActive(false);
        //webcam.Play();
        ca_app = true;
        //gameObject.GetComponent<ButtonScript>().enabled = true;
        Debug.Log("카메라가 켜집니다.");

        em_t.SetActive(false);

    }

    public void Go_Picture()
    {
        webcam.Pause();
        picture.SetActive(true);
    }
    public void Out_Picture()
    {
        GameObject.Find("MainManager").GetComponent<MainManager>().SetDaeyangFace(0);

        webcam.Play();
        //ca_intro.SetActive(true);
        //btns.SetActive(true);
        picture.SetActive(false);

    }

    public void Close_App()
    {
        //webcam.Play();
        ca_app = false;
        Debug.Log("카메라가 꺼집니다.");

        em_s.GetComponent<Text>().text = "";
        em_t.SetActive(true);
    }

    public void Take_Picture()
    {
        StopCoroutine("CheckTime");
        StartCoroutine("CheckTime");

        count.SetActive(true);  

        //webcam.Play();
        ca_button = true;

        // 하단 버튼 비활성화
        ca_intro.SetActive(false);
        btns.SetActive(false);


        //표정 활성화
        //GameObject.Find("EM").SetActive(true);
        //하단 문구 활성화
        //var color = GameObject.Find("EM_St").GetComponent<Text>().color;
        //color.a = 255;

        Debug.Log("사진을 촬영합니다.");
        if (em_s.GetComponent<Text>().text == "")
        {
            em_s.GetComponent<Text>().text = "얼굴을 인식해주세요!";
        }
    }
}