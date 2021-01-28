using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using epoching.easy_gui;
namespace epoching.easy_qr_code
{
    public class Generate_qr_code : MonoBehaviour
    {

        [Header("inputfiled")]
        public InputField input_field;

        [Header("qr code RawImage")]
        public RawImage raw_image_qr_code;

        void OnEnable()
        {
            this.raw_image_qr_code.gameObject.SetActive(false);
            this.input_field.text = "";
        }

        private Color32[] encode_info(string textForEncoding, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }

        public Texture2D generate_qr_code(string text)
        {
            var encoded = new Texture2D(256, 256);
            var color32 = encode_info(text, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            return encoded;
        }

        //event 
        #region 

        //generate button
        public void on_generate_btn()
        {
            if (this.input_field.text == null || this.input_field.text == "")
            {
                Canvas_confirm_box.confirm_box
                (
                    "Hint",
                    "Input field can not be empty",
                    "cancel",
                    "OK",
                    true,
                    delegate ()
                    {
                    },
                    delegate ()
                    {

                    }
                );
                return;
            }


            this.raw_image_qr_code.texture = this.generate_qr_code(this.input_field.text);
            this.raw_image_qr_code.gameObject.SetActive(true);

        }

        #endregion
    }
}
