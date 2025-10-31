using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;
using System.Configuration;


using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace speechto
{
    public partial class Form1 : Form
    {
        SpeechRecognizer recognizer;

        public Form1()
        {
            InitializeComponent();
            btnDurdur.Enabled = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblDurum.Text = "Durum: Azure'a bağlanmaya hazır.";
        }
        private async void btnBaslat_Click(object sender, EventArgs e)
        {
            string subscriptionKey = ConfigurationManager.AppSettings["AzureSpeechKey"];
            string region = ConfigurationManager.AppSettings["AzureSpeechRegion"];
            var config = SpeechConfig.FromSubscription(subscriptionKey, region);
            config.SpeechRecognitionLanguage = "tr-TR";
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                recognizer = new SpeechRecognizer(config, audioConfig);
            }
            recognizer.Recognized += Recognizer_Recognized; 
            recognizer.SessionStarted += Recognizer_SessionStarted; 
            recognizer.SessionStopped += Recognizer_SessionStopped; 
            await recognizer.StartContinuousRecognitionAsync();

            btnBaslat.Enabled = false;
            btnDurdur.Enabled = true;
        }
        private async void btnDurdur_Click(object sender, EventArgs e)
        {
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Recognized -= Recognizer_Recognized;
            recognizer.SessionStarted -= Recognizer_SessionStarted;
            recognizer.SessionStopped -= Recognizer_SessionStopped;
            recognizer.Dispose();
            recognizer = null;

            btnBaslat.Enabled = true;
            btnDurdur.Enabled = false;
            lblDurum.Text = "Durum: Durduruldu.";
        }
        private void Recognizer_SessionStarted(object sender, SessionEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lblDurum.Text = "Durum: Azure dinliyor...";
            }));
        }
        private void Recognizer_SessionStopped(object sender, SessionEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lblDurum.Text = "Durum: Oturum kapandı.";
            }));
        }
        private void Recognizer_Recognized(object sender, SpeechRecognitionEventArgs e)
        {
            if (e.Result.Reason == ResultReason.RecognizedSpeech)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    rtbMetin.AppendText(e.Result.Text + " ");
                }));
            }
            else if (e.Result.Reason == ResultReason.NoMatch)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    lblDurum.Text = "Durum: Konuşma anlaşılamadı.";
                }));
            }
        }
    }
}