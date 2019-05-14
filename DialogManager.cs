using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogText;
    public Text picNameText;
    public Text picDialogText;
    public Text promptHeadingText;
    public Text promptOption1;
    public Text promptOption2;
    public Text promptOption3;
    public Text promptOption4;
    public RectTransform promptCursor;
    public Animator dialogBoxAnimator;
    public Animator picDialogBoxAnimator;
    private RectTransform messageWindow;
    private RectTransform picMessageWindow;
    private Image picImageAvatar;
    
    private Queue<string> sentences;
    private bool isRunning;

    // for prompt window
    private Animator promptAnimator;
    public Vector3 promptCursorPos1;
    public Vector3 promptCursorPos2;
    public Vector3 promptCursorPos3;
    public Vector3 promptCursorPos4;
    public int numOfOptions = 2;

    public void Start()
    {
        messageWindow = GameObject.Find("MessageWindow").GetComponent<RectTransform>();
        picMessageWindow = GameObject.Find("PicMessageWindow").GetComponent<RectTransform>();
        picImageAvatar = GameObject.Find("MsgAvatar").GetComponent<Image>();
        promptAnimator = GameObject.Find("PromptWindow").GetComponent<Animator>();
        promptCursor = GameObject.Find("PromptCursor").GetComponent<RectTransform>();
        
        promptCursorPos1 = new Vector3(-143.5f, -2f, 0);
        promptCursorPos2 = new Vector3(-143.5f, -22.9f, 0);
        promptCursorPos3 = new Vector3(-32.5f, -2f, 0);
        promptCursorPos4 = new Vector3(-32.5f, -22.9f, 0);
    }

    public void dialog(string name, string message)
    {        
        if (!isRunning)
        {
            
            dialogBoxAnimator.SetFloat("isOpen", 1.0f);
            isRunning = true; 
        }
        
        nameText.text = name;
        
        StopAllCoroutines();
        StartCoroutine(typeSentence(message));
    }
    
    public void dialog(string name, string message, float height)
    {        
        if (!isRunning)
        {
            messageWindow.localPosition = new Vector3(messageWindow.localPosition.x, height, messageWindow.localPosition.z);
            dialogBoxAnimator.SetFloat("isOpen", 1.0f);
            isRunning = true; 
        }
        
        nameText.text = name;
        
        StopAllCoroutines();
        StartCoroutine(typeSentence(message));
    }
    
    public void picDialog(string name, string message, Sprite avatar, float height)
    {        
        if (!isRunning)
        {
            picImageAvatar.sprite = avatar;
            picMessageWindow.localPosition = new Vector3(picMessageWindow.localPosition.x, height, picMessageWindow.localPosition.z);
            picDialogBoxAnimator.SetFloat("isOpen", 1.0f);
            isRunning = true; 
        }
        
        picNameText.text = name;
        
        StopAllCoroutines();
        StartCoroutine(picTypeSentence(message));
    }
    
    public void picDialog(string name, string message, Sprite avatar)
    {        
        if (!isRunning)
        {
            picImageAvatar.sprite = avatar;
            picDialogBoxAnimator.SetFloat("isOpen", 1.0f);
            isRunning = true; 
        }
        
        picNameText.text = name;
        
        StopAllCoroutines();
        StartCoroutine(picTypeSentence(message));
    }

    public void promptDialog(string heading, params string[] options)
    {
        if (!isRunning)
        {
            isRunning = true;
            
            promptAnimator.SetFloat("isOpen", 1.0f);
            numOfOptions = options.Length;
            promptCursor.localPosition = promptCursorPos1;
            
            promptHeadingText.text = heading;
            promptOption1.text = options[0];
            promptOption2.text = options[1];
            promptOption3.text = numOfOptions >= 3 ? options[2] : null;
            promptOption4.text = numOfOptions >= 4 ? options[3] : null;
        }
    }
    
    private IEnumerator typeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
    
    private IEnumerator picTypeSentence(string sentence)
    {
        picDialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            picDialogText.text += letter;
            yield return null;
        }
    }

    public void endDialog()
    {
        dialogBoxAnimator.SetFloat("isOpen", -1.0f);
        isRunning = false;
        nameText.text = "";
        dialogText.text = "";
    }
    
    public void endPicDialog()
    {
        picDialogBoxAnimator.SetFloat("isOpen", -1.0f);
        isRunning = false;
        picNameText.text = "";
        picDialogText.text = "";
    }
    
    public void endPromptDialog()
    {
        promptAnimator.SetFloat("isOpen", -1.0f);
        isRunning = false;
        promptHeadingText.text = "";
        promptOption1.text = "";
        promptOption2.text = "";
        promptOption3.text = "";
        promptOption4.text = "";
    }

    public bool getIsRunning()
    {
        return isRunning;
    }
}
