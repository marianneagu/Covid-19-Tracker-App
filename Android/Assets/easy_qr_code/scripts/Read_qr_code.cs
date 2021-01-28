using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using epoching.easy_debug_on_the_phone;
using UnityEngine.UI;
using epoching.easy_gui;

namespace epoching.easy_qr_code
{
    public class Read_qr_code : MonoBehaviour
    {
        [Header("raw_image_video")]
        public RawImage raw_image_video;

        [Header("audio source")]
        public AudioSource audio_source;

        //camera texture
        private WebCamTexture cam_texture;

        //is reading qr_code
        private bool is_reading = true;

        void OnEnable()
        {
            try
            {
                this.is_reading = true;

                //init camera texture
                this.cam_texture = new WebCamTexture();

                //if (cam_texture != null)
                //{
                this.cam_texture.Play();


                if (Application.platform == RuntimePlatform.Android)
                {
                    this.raw_image_video.rectTransform.sizeDelta = new Vector2(Screen.width * cam_texture.width / (float)this.cam_texture.height, Screen.width);
                    this.raw_image_video.rectTransform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    this.raw_image_video.rectTransform.sizeDelta = new Vector2(1080, 1080 * this.cam_texture.width / (float)this.cam_texture.height);
                    this.raw_image_video.rectTransform.localScale = new Vector3(-1, 1, 1);
                    this.raw_image_video.rectTransform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    this.raw_image_video.rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelWidth * this.cam_texture.height / (float)this.cam_texture.width);
                    this.raw_image_video.rectTransform.localScale = new Vector3(-1, 1, 1);
                }

                this.raw_image_video.texture = cam_texture;
                //}
            }
            catch (Exception ex)
            {
                Canvas_confirm_box.confirm_box("confirm box", "No camera detected", "cancel", "OK", true, delegate () { }, delegate ()
                {
                   
                });

                throw;
            }
        }

        private float interval_time = 1f;
        private float time_stamp = 0;
        void Update()
        {
            if (this.is_reading)
            {
                this.time_stamp += Time.deltaTime;

                if (this.time_stamp > this.interval_time)
                {
                    this.time_stamp = 0;

                    try
                    {
                        IBarcodeReader barcodeReader = new BarcodeReader();
                        // decode the current frame
                        var result = barcodeReader.Decode(this.cam_texture.GetPixels32(), this.cam_texture.width, this.cam_texture.height);
                        if (result != null)
                        {
                            Canvas_confirm_box.confirm_box
                            (
                                "Detect QR code",
                                result.Text,
                                "Cancel",
                                "Update",
                                 delegate () 
                                 {
                                     this.is_reading = true;
                                 },
                                 delegate ()
                                 {
                                     Debug.Log("Update");
                                     //Aici urmeaza logica pentru a updata baza de date
                                     ScannedQR.public_id = result.Text.ToString();
                                     ScannedQR.startInstert = true;
                                     //Application.OpenURL(result.Text);
                                     this.is_reading = true;
                                 }
                            );
                            Debug.Log("DECODED TEXT FROM QR: " + result.Text);

                            this.is_reading = false;

                            this.audio_source.Play();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning(ex.Message);
                        Canvas_confirm_box.confirm_box
                        (
                            "confirm box",
                            "error>>>"+ ex.Message, 
                            "cancel", 
                            "OK",
                            true, 
                            delegate (){},
                            delegate (){}
                        );
                    }
                }
            }
        }
    }
}

