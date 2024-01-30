using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace Test
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        string[] options;
        int curr_level = 0;
        int seconds = 0;
        String lifeline_used = "";
        String selected = "";
        String selected_label = "";
        int curr_level_copy = 0;

        string level;
        QuestionDTO question;

        int[] score = { 0, 5, 10, 20, 40, 80, 100, 200, 300, 400, 500 };
        public Form1()
        {
            InitializeComponent();
            cancel_1.BackColor = Color.Transparent;
            cancel_1.Parent = lifeline_1;
            cancel_1.Location = new Point(0, 0);
            cancel_1.Visible = false;

            cancel_2.BackColor = Color.Transparent;
            cancel_2.Parent = lifeline_2;
            cancel_2.Location = new Point(0, 0);
            cancel_2.Visible = false;

            level10.Enabled = false;
            level9.Enabled = false;
            level8.Enabled = false;
            level7.Enabled = false;
            level6.Enabled = false;
            level5.Enabled = false;
            level4.Enabled = false;
            level3.Enabled = false;
            level2.Enabled = false;
            level1.BackColor = Color.Green;
            //lifeline_panel.Visible = false;
            qn_panel.Visible = false;
            confirmation_panel.Visible = false;
            //timer_box.Visible = false;
            option_panel.Visible = false;
            show_option.Visible=false;
            lock_answer.Visible = false;
            image.Visible = false;
            audio.Visible = false;

            var pos_a = option_a_text.Parent.PointToScreen(option_a_text.Location);
            pos_a = option_a.PointToClient(pos_a);
            option_a_text.Parent = option_a;
            option_a_text.Location = pos_a;
            option_a_text.BackColor = Color.Transparent;

            var pos_b = option_b_text.Parent.PointToScreen(option_b_text.Location);
            pos_b = option_b.PointToClient(pos_b);
            option_b_text.Parent = option_b;
            option_b_text.Location = pos_b;
            option_b_text.BackColor = Color.Transparent;

            var pos_c = option_c_text.Parent.PointToScreen(option_c_text.Location);
            pos_c = option_c.PointToClient(pos_c);
            option_c_text.Parent = option_c;
            option_c_text.Location = pos_c;
            option_c_text.BackColor = Color.Transparent;

            var pos_d = option_d_text.Parent.PointToScreen(option_d_text.Location);
            pos_d = option_d.PointToClient(pos_d);
            option_d_text.Parent = option_d;
            option_d_text.Location = pos_d;
            option_d_text.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void lifeline_1_Click(object sender, EventArgs e)
        {
            lifeline_used = "50-50";
            confirmation_panel.Visible = true;
            label1.Text = "50:50 ?";
            label2.Text = "";
        }

        private void lifeline_2_Click(object sender, EventArgs e)
        {
            lifeline_used = "audience_poll";
            confirmation_panel.Visible = true;
            label1.Text = ""; 
            label2.Text = "Audience Poll ?";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Text = seconds--.ToString();
            if (seconds < 1)
            {
                timer1.Stop();
                MessageBox.Show("Times up!");
            }
        }

        private void level1_Click(object sender, EventArgs e)
        {
            timer.Visible = false;
            timer_box.Visible = false;

            option_a_text.Visible = false;
            option_b_text.Visible = false;
            option_c_text.Visible = false;
            option_d_text.Visible = false;

            image.Visible = false;
            audio.Visible = false;

            lifeline_panel.Visible = true;
            timer.Text = "60";
            qn_panel.Visible = true;
            lifeline_panel.Visible = true;
            lifeline_1.Enabled = false;
            lifeline_2.Enabled = false;
            show_option.Enabled = true;

            show_option.Visible = true;

            option_a.ImageLocation = Environment.CurrentDirectory + @"\Images\L_option.png";
            option_b.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";
            option_c.ImageLocation = Environment.CurrentDirectory + @"\Images\L_option.png";
            option_d.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";

            var levelButton = (System.Windows.Forms.Button)sender;
            levelButton.Enabled = false;

            level = "" + levelButton.Text;
            level = "" + Array.IndexOf(score, Convert.ToInt32(level));
            fetch_data obj1 = new fetch_data();
            question = obj1.FetchQuestion(level);
            qtext.Text = question.question;
            option_a_text.Text = question.option1;
            option_b_text.Text = question.option2;
            option_c_text.Text = question.option3;
            option_d_text.Text = question.option4;

            if (question.image != "")
            {
                image.Visible = true;
                image.ImageLocation = Environment.CurrentDirectory + @"\Images\" + question.image;
            }

            if (question.audio != "")
            {
                audio.Visible = true;
            }
        }

        private void audio_Click(object sender, EventArgs e)
        {
            /*SoundPlayer my_wave_file = new SoundPlayer("D:\\Visual Studio 2022 Works CEI\\Wfh copy\\Test\\Audio\\"+aud);
            my_wave_file.PlaySync();*/

            WindowsMediaPlayer wmp = new WindowsMediaPlayer();

            // You can subscribe to events if needed (e.g., wmp.PlayStateChange)

            wmp.URL = Environment.CurrentDirectory + @"\Audio\"+question.audio;
            wmp.controls.play();
        }

        void enable_next_level()
        {
            
            curr_level_copy = curr_level;
            curr_level++;
            string next_level="level"+curr_level.ToString();
            if (this.Controls.Find(next_level, true).FirstOrDefault() is System.Windows.Forms.Button button)
            {
                button.Enabled = true;
                button.BackColor = Color.Green;
            }
                
        }
        void reset()
        {
            level2.BackColor = Color.White;
            level3.BackColor = Color.White;
            level4.BackColor = Color.White;
            level5.BackColor = Color.White;
            level6.BackColor = Color.White;
            level7.BackColor = Color.White;
            level8.BackColor = Color.White;
            level9.BackColor = Color.White;
            level10.BackColor = Color.White;
            level10.Enabled = false;
            level9.Enabled = false;
            level8.Enabled = false;
            level7.Enabled = false;
            level6.Enabled = false;
            level5.Enabled = false;
            level4.Enabled = false;
            level3.Enabled = false;
            level2.Enabled = false;
            level1.Enabled = true;
            level1.BackColor = Color.Green;

            label3.Visible = false;

            qn_panel.Visible = false;

            confirmation_panel.Visible = false;
            option_panel.Visible = false;
            show_option.Visible = false;
            lock_answer.Visible = false;

            timer.Visible = false;
            timer_box.Visible = false;
            timer.Text = "60";

            option_a_text.Visible = false;
            option_b_text.Visible = false;
            option_c_text.Visible = false;
            option_d_text.Visible = false;

            image.Visible = false;
            audio.Visible = false;

            cancel_1.Visible = false;
            cancel_2.Visible = false;

            option_panel.Visible=false; 
            lock_answer.Visible = false;

            option_a.ImageLocation= Environment.CurrentDirectory + @"\Images\L_option.png";
            option_b.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";
            option_c.ImageLocation = Environment.CurrentDirectory + @"\Images\L_option.png";
            option_d.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";
        }

        private void lock_answer_Click(object sender, EventArgs e)
        {
            if (selected.Equals(question.correctAnswer, StringComparison.InvariantCultureIgnoreCase))
            {     
                if (this.Controls.Find(selected_label, true).FirstOrDefault() is PictureBox pictureBox)
                {
                    if(selected_label=="option_a" || selected_label=="option_c")
                        pictureBox.ImageLocation = Environment.CurrentDirectory + @"\Images\crct_L_option.png";

                    if (selected_label == "option_b" || selected_label == "option_d")
                        pictureBox.ImageLocation = Environment.CurrentDirectory + @"\Images\crct_R_option.png";

                    if (curr_level == 10)
                    {
                        label3.Text = "Score: 500 !";
                        lifeline_1.Enabled = false;
                        lifeline_2.Enabled = false;
                        option_a_text.Enabled = false; option_b_text.Enabled = false; option_c_text.Enabled = false; option_d_text.Enabled = false;
                        show_option.Enabled = false;
                        lock_answer.Enabled = false;

                    }
                    else
                        enable_next_level();

                }
            }
            else
            {
                if (this.Controls.Find(selected_label, true).FirstOrDefault() is PictureBox pictureBox)
                {
                    if (selected_label == "option_a" || selected_label == "option_c")
                        pictureBox.ImageLocation = Environment.CurrentDirectory + @"\Images\wrong_L_option.png";

                    if (selected_label == "option_b" || selected_label == "option_d")
                        pictureBox.ImageLocation = Environment.CurrentDirectory + @"\Images\wrong_R_option.png";

                }

                if(option_a_text.Text.Equals(question.correctAnswer, StringComparison.InvariantCultureIgnoreCase))
                    option_a.ImageLocation=Environment.CurrentDirectory + @"\Images\crct_L_option.png";
                if (option_b_text.Text.Equals(question.correctAnswer, StringComparison.InvariantCultureIgnoreCase))
                    option_b.ImageLocation = Environment.CurrentDirectory + @"\Images\crct_R_option.png";
                if (option_c_text.Text.Equals(question.correctAnswer, StringComparison.InvariantCultureIgnoreCase))
                    option_c.ImageLocation = Environment.CurrentDirectory + @"\Images\crct_L_option.png";
                if (option_d_text.Text.Equals(question.correctAnswer, StringComparison.InvariantCultureIgnoreCase))
                    option_d.ImageLocation = Environment.CurrentDirectory + @"\Images\crct_R_option.png";

            }

            label3.Visible = true;
            label3.Text = score[curr_level - 1].ToString();
            timer1.Stop();
        }
        public void answer_checked()
        {
            option_a.ImageLocation = Environment.CurrentDirectory + @"\Images\L_option.png";
            option_b.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";
            option_c.ImageLocation = Environment.CurrentDirectory + @"\Images\L_option.png";
            option_d.ImageLocation = Environment.CurrentDirectory + @"\Images\R_option.png";
        }

        private void option_a_text_Click(object sender, EventArgs e)
        {
            selected = option_a_text.Text.ToString();
            selected_label = "option_a";
            answer_checked();
            option_a.ImageLocation = Environment.CurrentDirectory + @"\Images\checked_L_option.png";
        }

        private void option_b_text_Click(object sender, EventArgs e)
        {
            selected = option_b_text.Text.ToString();
            selected_label = "option_b";
            answer_checked();
            option_b.ImageLocation = Environment.CurrentDirectory + @"\Images\checked_R_option.png";
        }

        private void option_c_text_Click(object sender, EventArgs e)
        {
            selected = option_c_text.Text.ToString();
            selected_label = "option_c";
            answer_checked();
            option_c.ImageLocation = Environment.CurrentDirectory + @"\Images\checked_L_option.png";
        }

        private void option_d_text_Click(object sender, EventArgs e)
        {
            selected = option_d_text.Text.ToString();
            selected_label = "option_d";
            answer_checked();
            option_d.ImageLocation = Environment.CurrentDirectory + @"\Images\checked_R_option.png";
        }

        private void image_Click(object sender, EventArgs e)
        {

        }
        

        private void confirmation_close_Click(object sender, EventArgs e)
        {
            confirmation_panel.Visible = false;
        }

        private void confirm_yes_Click_1(object sender, EventArgs e)
        {
            //timer1.Stop();
            if (lifeline_used.Equals("50-50"))
            {
                cancel_1.Visible = true;
                if (option_a_text.Text == question.correctAnswer)
                {
                    options = new string[] { "option_b_text", "option_c_text", "option_d_text" };
                    int randomIndex = random.Next(0, options.Length);
                    string randomValue = options[randomIndex];
                    string[] newArray = options.Where(value => value != randomValue).ToArray();
                    foreach (string S in newArray)
                    {
                        if (this.Controls.Find(S, true).FirstOrDefault() is System.Windows.Forms.Label label)
                        {
                            label.Visible = false;
                        }
                    }
                }
                if (option_b_text.Text == question.correctAnswer)
                {
                    options = new string[] { "option_a_text", "option_c_text", "option_d_text" };
                    int randomIndex = random.Next(0, options.Length);
                    string randomValue = options[randomIndex];
                    string[] newArray = options.Where(value => value != randomValue).ToArray();
                    foreach (string S in newArray)
                    {
                        if (this.Controls.Find(S, true).FirstOrDefault() is System.Windows.Forms.Label label)
                        {
                            label.Visible = false;
                        }
                    }
                }
                if (option_c_text.Text == question.correctAnswer)
                {
                    options = new string[] { "option_b_text", "option_a_text", "option_d_text" };
                    int randomIndex = random.Next(0, options.Length);
                    string randomValue = options[randomIndex];
                    string[] newArray = options.Where(value => value != randomValue).ToArray();
                    foreach (string S in newArray)
                    {
                        if (this.Controls.Find(S, true).FirstOrDefault() is System.Windows.Forms.Label label)
                        {
                            label.Visible = false;
                        }
                    }
                }
                if (option_d_text.Text == question.correctAnswer)
                {
                    options = new string[] { "option_b_text", "option_c_text", "option_a_text" };
                    int randomIndex = random.Next(0, options.Length);
                    string randomValue = options[randomIndex];
                    string[] newArray = options.Where(value => value != randomValue).ToArray();
                    foreach (string S in newArray)
                    {
                        if (this.Controls.Find(S, true).FirstOrDefault() is System.Windows.Forms.Label label)
                        {
                            label.Visible = false;
                        }
                    }
                }

                }
            if (lifeline_used.Equals("audience_poll"))
                cancel_2.Visible = true;
            confirmation_panel.Visible = false;
        }

        private void confirm_no_Click_1(object sender, EventArgs e)
        {
            confirmation_panel.Visible = false;
        }

        private void show_option_Click(object sender, EventArgs e)
        {
            option_a_text.Visible = true;
            option_b_text.Visible = true;
            option_c_text.Visible = true;
            option_d_text.Visible = true;

            timer.Visible = true;
            timer_box.Visible = true;
            timer.Text = "";
            seconds = 60; 
            timer1.Start();
            option_panel.Visible = true;
            lock_answer.Visible = true;
            curr_level = Convert.ToInt32(level);
            lock_answer.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lock_answer_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
