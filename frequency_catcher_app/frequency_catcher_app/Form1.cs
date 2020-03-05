using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frequency_catcher_app
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public static string freq;
        public static string answer;
        public string freq_name;
        public string path;
        public static decimal QuantityOfFiles;
        public string AllPossibleAnswers;
        public static  int QuantityOfQuestions;
        public string[] TabOfFiles = new string[100];
        public int ask;
        private bool ButtonCheckWasClicked = true;
        public string wzorzec_path;
        public int q;
        private int goodAnswer;
        private int wrongAnswer;


        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            
        }

       
       
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            numericUpDown2.Enabled = true;
            button4.Enabled = true;
           
                       

            for (int i = 0; i < decimal.ToInt32(QuantityOfFiles); ++i)
            {
                textBox3.Text = "Read " + (decimal.ToInt32(QuantityOfFiles) - i).ToString() + " more files.";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Audio Files (.wav)|*.wav";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.FileName;
                    if (checkBox1.Checked == true)
                    {
                        playSound(path);
                    }
                    OpenFileDialog name = new OpenFileDialog();
                    freq_name = read_freq(path);
                    // MessageBox.Show(freq_name);
                    TabOfFiles[i] = path;
                    textBox2.Text += freq_name;
                    textBox2.Text += "   ";
                }
                else
                {
                    QuantityOfFiles = i;
                    break;
                }
                textBox3.Clear();
            }
            
            button1.Enabled = false;
            textBox3.Visible = true;
        }


        public string read_freq(string path)
        {
            int a = path.LastIndexOf('\\') + 1;
            string freq_wav = path.Substring(a);
            freq = freq_wav.Replace(".wav", "");
            if(freq.Contains("Hz"))
            {
                int index = freq.IndexOf("Hz");
                if (index > 0)
                    freq = freq.Substring(0, index);
            }
           
            
            return freq;
        }

        private void playSound(string path)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = path;
            player.Load();
            player.Play();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            answer = textBox1.Text;
            if(answer == null)
            {
                MessageBox.Show("C'mon, give some answer");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string info = q.ToString()+"/"+QuantityOfQuestions.ToString()+"\n";
            
            if (answer.Equals(read_freq(TabOfFiles[ask])))
            {
                info += "That's right"+"\nYour answer:"+answer+"\nRight answer:"+ read_freq(TabOfFiles[ask]);
                ++goodAnswer;
                textBox1.BackColor = Color.Green;
                
            }
            else
            {
                info += "Sorry, but you're wrong\n The answer is:" + read_freq(TabOfFiles[ask]) + "." + "\nYour answer: " + answer;
                ++wrongAnswer;
                textBox1.BackColor = Color.Red;
                
            }
            DialogResult result = MessageBox.Show(info);
            label6.Text = goodAnswer.ToString();
            label7.Text = wrongAnswer.ToString();
            if (result == DialogResult.OK && q < QuantityOfQuestions)
            {
                
                NextQuestions();
            }
            else
            {
                button6.Enabled = false;
                button2.Enabled = false;
                double score = ((double)goodAnswer / (double)QuantityOfQuestions)*100;
                string score_info = "The End" + "\nYour score: " + score.ToString() + "%"+"\n";
                if (score < 75)
                {
                    score_info += "You fail, but don't worry, try again";
                    BackColor = Color.Red; }
                else
                {
                    score_info += "Yeah, you pass";
                    BackColor = Color.Green;
                }
                MessageBox.Show(score_info);
                if (result == DialogResult.OK )
                {
                    this.BackColor = Color.WhiteSmoke;
                    textBox1.BackColor = Color.WhiteSmoke;
                }
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            QuantityOfFiles = numericUpDown1.Value;
            textBox2.Clear();
            //złapać wyjątek :P
            int z = decimal.ToInt32(QuantityOfFiles);
            if (QuantityOfFiles <= 0)
            {
                MessageBox.Show("Niepoprawna wartość");
            }
            else
            {
                button1.Enabled = true;
                textBox3.Visible = true;
                textBox3.Text = "Read " + (decimal.ToInt32(QuantityOfFiles)).ToString() + " more files.";
            }
            
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            QuantityOfQuestions = decimal.ToInt32(numericUpDown2.Value);
            if (QuantityOfQuestions < 1)
            {
                MessageBox.Show("Number of questions must be more than 0");
            }
            else
            {
                button5.Enabled = true;
                string info = "Files: " + QuantityOfFiles.ToString() + "\nAsks: " + QuantityOfQuestions;
                MessageBox.Show(info);
            }

        }

        public void Quiz()
        {
            
            label6.Text = "";
            label7.Text = "";
            goodAnswer = 0;
            wrongAnswer = 0;
            textBox2.Enabled = true;


                q = 0;
                
                NextQuestions();

                
             

        }
        public void NextQuestions()
        {
            textBox1.BackColor = Color.WhiteSmoke;
            

            if (q < QuantityOfQuestions)
            {
                 
                ask = rnd.Next(0, decimal.ToInt32(QuantityOfFiles));
                //this.BackColor = Color.BlueViolet;
                //playSound(wzorzec_path);
                //this.BackColor = Color.WhiteSmoke;

                playSound(TabOfFiles[ask]);
                string info = "ask = " + ask.ToString() + "\n" + TabOfFiles[ask].ToString();
                ButtonCheckWasClicked = false;
            }
            else
            {
                MessageBox.Show("The End");
            }
            ++q;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //.Enabled = false;
            textBox1.Enabled = true;
            button2.Enabled = true;
            button6.Enabled = true;
           // textBox2.Enabled = true;

            Quiz();
            
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            playSound(TabOfFiles[ask]);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Audio Files (.wav)|*.wav";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    wzorzec_path = dialog.FileName;

                    OpenFileDialog name = new OpenFileDialog();

                }
                if (checkBox1.Checked == true)
                {
                    playSound(wzorzec_path);
                }
           
            }
            
        }
    }
}


// wypisać możliwe odpowiedzi=> zamiast stringa zrobić listę i posortować i przesuwacz, aby moznabyło wszystkie zobaczyć
//ikonka applikacji i przy score obraski
//zamykanie esc i enter jako chexk
//ew buttony dla każdej odpowiedzi zamiast wpisywania
//timer
//dezaktywowanie i uaktywnianie przycisków
//statystyki?? mądre losowanie pytań, bazujące na poprawnych odpowiedziach 
//wyeksportować do .exe :D + aktuaizacje z local hosta

