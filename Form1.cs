using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ultimate_Predictor
{
    public partial class Form1 : Form
    {
        private const string AppName = "Предсказатель";
        private readonly string PredictionConfigPath = $"{Environment.CurrentDirectory}\\predictionConfig.json";
        public string[] Predictions;
        private Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            bPredict.Enabled = false;
            await Task.Run(() =>
            {

                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"{i}%";
                    }));
                    
                    Thread.Sleep(37); // Имитирую бурную работу программы и сложную логику
                }
            });

            // Рандомный выбор предсказания
            var index = _random.Next(Predictions.Length);
            var predictions = Predictions[index];
            MessageBox.Show($"{predictions}!");

            progressBar1.Value = 0;
            this.Text = AppName;

            bPredict.Enabled = true;
        }
        /// <summary>
        /// Метод для лечения бага заполняющей строки, как работает не понимаю
        /// </summary>
        /// <param name="intValue"></param>
        private void UpdateProgressBar(int intValue)
        {
            if (intValue == progressBar1.Maximum)
            {
                progressBar1.Maximum = intValue + 1;
                progressBar1.Value = intValue + 1;
                progressBar1.Maximum = intValue;
            }
            else
            {
                progressBar1.Value = intValue + 1;
            }
            progressBar1.Value = intValue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = AppName;
            try
            {
                var data = File.ReadAllText(PredictionConfigPath);

                Predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Predictions == null)
                {
                    Close();
                }
                else if (Predictions.Length == 0)
                {
                    MessageBox.Show("Предсказания закончились, приходите в другой раз");
                }
            }
        }
    }
}
