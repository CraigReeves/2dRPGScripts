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
    public Animator dialogBoxAnimator;
    public Animator picDialogBoxAnimator;
    private RectTransform messageWindow;
    private RectTransform picMessageWindow;
    private Image picImageAvatar;
    
    private Queue<string> sentences;
    private bool isRunning;

    public void Start()
    {
        messageWindow = GameObject.Find("MessageWindow").GetComponent<RectTransform>();
        picMessageWindow = GameObject.Find("PicMessageWindow").GetComponent<RectTransform>();
        picImageAvatar = GameObject.Find("MsgAvatar").GetComponent<Image>();
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

    public bool getIsRunning()
    {
        return isRunning;
    }
}
