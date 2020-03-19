using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
THIS HAS BECOME A MEGA FILE BASED ON THE ADVICE OF EDWARD JAMES GARMON
*/

public class MainScript : MonoBehaviour
{

    public movementMan movescript;
    public GameObject cam_Broad;
    public Transform broad_Point;
    public Transform jerry_Point;
    public Transform george_Point;
    public Transform elane_Point;

    //temporary, locations will be given via json later on
    public Vector3 broadLoc = new Vector3(-2.31f, 1.58f, -9.38f);
    public Vector3 jerrLoc = new Vector3(-3.35f, 2.86f, -5.5f);
    public Vector3 georgeLoc = new Vector3(1.95f, 2.09f, -6.89f);
    public Vector3 elaneLoc = new Vector3(-5.9f, 2.03f, -5.85f);

    [Serializable]
    public class line
    {
        public string characterName;
        public string eventType;
        public string fileName;
        public double duration;
        public float startTime;
    }

    [Serializable]
    public class movement
    {
        
        public string eventType;
        public string character;

        [Serializable]
        public class node
        {
            public string character;
            public string name;
            public int[] location;
        }

    }

    [Serializable]
    public class node
    {
        public string character;
        public int[] location;
        public string name;
    }

    [Serializable]
    public class genericEvent
    {
        public string characterName;
        public string eventType;
        public string fileName;
        public double duration;
        public float startTime;
        public float delay;
        public string character;

        public string shotType;
        public node node;

        [Serializable]
        public class camera
        {
            public string name;
            public Vector3 location;
        }


    }

    [Serializable]
    public class data
    {
        public string[] characters;
      
    }

    [Serializable]
    public class camEvent
    {
        public string eventType;
        public string shotType;
        public float startTime;

        [Serializable]
        public class camera
        {
            public string name;
            public Vector3 location;
        }

    }

    public static class genJsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.events;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.events = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.events = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] events;
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Events;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Events = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Events = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Events;
        }
    }

    public static class JsonHelperTest //merge with the main helper ASAP
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.setData;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.setData = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.setData = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] setData;
        }
    }

    public static class JsonHelperCam //merge with the main helper ASAP
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.cameraEvents;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.cameraEvents = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.cameraEvents = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] cameraEvents;
        }
    }

    string path;
    string jsonString;

    // Start is called before the first frame update
    void Start()
    {

        //var clip = Resources.Load("laughs/a_laugh track 1_15.wav") as AudioClip;

        StartCoroutine(startUp());

        path = "Assets/streamAssets/Scipt.json";
         jsonString = File.ReadAllText(path);

         line[] lines = JsonHelper.FromJson<line>(jsonString);

         data[] set = JsonHelperTest.FromJson<data>(jsonString);

         camEvent[] cams = JsonHelperCam.FromJson<camEvent>(jsonString);

         Debug.Log(set[0].characters[1]);

        StartCoroutine(GetText());
        /*
        for (int i = 0; i < lines.Length; i++)
         {
             if (lines[i].eventType == "LaughTrack")
             {
                StartCoroutine(playLaugh(lines[i].fileName, lines[i].startTime));
                StartCoroutine(saychar("audience", lines[i].startTime));
             }
             else
             {
                 StartCoroutine(DownloadAndPlay(lines[i].fileName, lines[i].startTime, lines[i].characterName));
                 StartCoroutine(saychar(lines[i].characterName, lines[i].startTime));
             }
         }

         for (int i = 0; i < cams.Length; i++)
         {
             StartCoroutine(manageCam(cams[i].shotType, cams[i].startTime));
         }
         */


     

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator startUp()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://scriptgen.herokuapp.com/generateEvents");
        yield return www.SendWebRequest();
    }

    IEnumerator DownloadAndPlay(string url, float delay, string schar)
    {
        yield return new WaitForSeconds(delay);
        WWW www = new WWW(url);
        yield return www;
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = www.GetAudioClip(false, true, AudioType.WAV);
        audio.PlayOneShot(audio.clip);
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://scriptgen.herokuapp.com/runEvents");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text


            // Or retrieve results as binary data
            string data = www.downloadHandler.text;
            Debug.Log(data);
            genericEvent[]  genericEvents = genJsonHelper.FromJson<genericEvent>(data);
            Debug.Log(genericEvents[0].eventType);

            StartCoroutine(execute(genericEvents));

        }
    }

    IEnumerator execute(genericEvent[] genericEvents)
    {
        float delay = 0;
        Debug.Log("pre loop");
        for (int i = 0; i < genericEvents.Length; i++)
        {
            if (genericEvents[i].eventType == "Camera" )
            {
                Debug.Log("camera should move");
                StartCoroutine(manageCam(genericEvents[i].shotType, 0));
            }
            else if (genericEvents[i].eventType == "CharacterLine")
            {
                StartCoroutine(DownloadAndPlay(genericEvents[i].fileName, 0, genericEvents[i].characterName));
                delay += genericEvents[i].delay;

            }
            else if (genericEvents[i].eventType == "LaughTrack")
            {
                StartCoroutine(playLaugh(genericEvents[i].fileName, 0));
                delay += genericEvents[i].delay;
            }

            else if (genericEvents[i].eventType == "movement")
            {
                Debug.Log("MOVEMENT DEBUG\n");
                movescript.active = true;
                movescript.seinfeldChar = genericEvents[i].character;
                movescript.targetString = genericEvents[i].node.name;
                //  movescript.character = genericEvents[i].character;
                //movescript.animationSource = .5f;
                ;
            }

        }

        yield return new WaitForSeconds(delay);
        StartCoroutine(GetText());


        yield return 0;
    }

    IEnumerator playLaugh(string path, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource audioLaugh = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load(path.Substring(0,path.Length-4)) as AudioClip;
        audioLaugh.clip = clip;
        audioLaugh.Play();

    }

    IEnumerator saychar(string characterName, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log(characterName);
    }

    IEnumerator manageCam(string shotType, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log(shotType);

        if (shotType == "BroadShot")
        {
            cam_Broad.transform.position = broadLoc;
            cam_Broad.transform.LookAt(broad_Point);
            //cam_Broad.transform.Translate(Vector3.up);
            

        }
        else if(shotType == "Focus - Jerry")
        {
            cam_Broad.transform.position = jerrLoc;
            cam_Broad.transform.LookAt(jerry_Point);
            //cam_Broad.transform.Translate(Vector3.up);
          
        }
        else if(shotType == "Focus - George")
        {
            cam_Broad.transform.position = georgeLoc;
            cam_Broad.transform.LookAt(george_Point);
            //cam_Broad.transform.Translate(Vector3.up);
           

        }
        else if(shotType == "Focus - Elaine" )
        {
            cam_Broad.transform.position = elaneLoc;
            cam_Broad.transform.LookAt(elane_Point);
            //cam_Broad.transform.Translate(Vector3.up);
            
        }

    }

    public void goBroad()
    {


    }
}
