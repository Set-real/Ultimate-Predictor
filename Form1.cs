using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ultimate_Predictor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void bPredict_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                this.Invoke(new Action(() =>
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        progressBar1.Value = i;
                        Thread.Sleep(200);
                    }
                }));
            });

            MessageBox.Show("Предсказание");
        }
    }
}
