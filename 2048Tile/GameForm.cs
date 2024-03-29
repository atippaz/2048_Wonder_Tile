﻿using System;
using System.Drawing;
using System.Windows.Forms;
namespace _2048Tile
{
    public partial class WonderTile : Form
    {
        int nTime = 0, nValue, nPosition, nScore = 0;
        int nCurrentTile, nNextTile;
        bool bGameOver = false, bTileMove = false;
        readonly Random rdRandom = new Random();
        readonly Label[] Tile = new Label[17];
        public WonderTile()
        {
            InitializeComponent();
        }

        //ปุ่มออกโปรแกรม
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //ปุ่มพับหน้าจอ
        private void MinimumBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //สุ่มค่ากะเบื้อง  2 - 4 โดยโอกาสอยู่ที่ 2 = 0.8% 4 = 0.2% 
        int RandomValue()
        {
            if (rdRandom.Next(10) + 1 >= 8)
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }
        //สุ่มกระเบื้องตั้งเเต่ตำเเหน่งที่ 1 - 16
        int RandomPosition()
        {
            return rdRandom.Next(16) + 1;
        }
        void GameStart()
        {
            //สร้างแบบจำลองกระเบื้อง 16 อัน เพื่อนเทียบกับกระเบื้องจริง
            for (int i = 1; i <= 16; i++)
            {
                Tile[i] = (Label)this.Controls.Find("tile" + i, true)[0];
                Tile[i].Text = "";
            }
            buttonRestart.Hide();
            nTime = 0;
            GameTime.Start();
            NewGame.Hide();
            for (int times = 1; times <= 2; times++)
            {
                nValue = RandomValue();
                nPosition = RandomPosition();
                Tile[nPosition].Text = nValue.ToString();
                ChangeBgColor();
            }
        }
        private void NewGame_Click(object sender, EventArgs e)
        {
            GameStart();
            //ให้กระเบื้องทุกอันมีค่าเป็น 0 (" ว่างเปล่า ")
        }
        void save()
        {
            //ให้กระเบื้องทุกอันมีค่าเป็น 0 (" ว่างเปล่า ")
            for (int i = 1; i <= 16; i++)
            {
                Tile[i] = (Label)this.Controls.Find("tile" + i, true)[0];
                Tile[i].Text = "2";
            }
            nTime = 0;
            GameTime.Start();
            /* nValue = RandomValue();
             nPosition = RandomPosition();
             Tile[nPosition].Text = nValue.ToString();*/
            ChangeBgColor();
            NewGame.Visible = false;
        }
        private void GameTimeEllasp(object sender, EventArgs e)
        {
            nTime++;
            int nSeconds = nTime % 60;
            int nMinutes = nTime / 60;
            string sTime = nMinutes.ToString("0#") + " Min" + " : " + nSeconds.ToString("0#") + " Sec";
            TimeShow.Text = sTime;
        }

        private void KeyIsDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                MoveUp();
            }

            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                MoveDown();
            }

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }

            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                MoveRight();
            }
        }
        private void RandomTile()
        {
            if (bTileMove == true)
            {
                do
                {
                    nPosition = RandomPosition();
                } while (Tile[nPosition].Text != "");
                nValue = RandomValue();
                Tile[nPosition].Text = nValue.ToString();
                ChangeBgColor();
            }
        }
        private void MoveRight()
        {
            bTileMove = false;
            GameOver();
            Right();
            for (int j = 4; j <= 16; j += 4)
            {
                for (int i = j; i > j - 3; i--)
                {
                    if (Tile[i].Text == Tile[i - 1].Text && Tile[i].Text != "")
                    {
                        nCurrentTile = Convert.ToInt32(Tile[i].Text);
                        nNextTile = Convert.ToInt32(Tile[i - 1].Text);
                        nCurrentTile += nNextTile;
                        Tile[i - 1].Text = nCurrentTile.ToString();
                        Tile[i].Text = "";
                        ChangeBgColor();
                        nScore += nCurrentTile;
                        Score.Text = nScore.ToString();
                        bTileMove = true;
                    }
                }
            }
            Right();
            RandomTile();
        }
        void Right()
        {
            for (int i = 4; i <= 16; i += 4)
            {
                for (int j = i; j >= i - 3; j--)
                {
                    int temp = j;
                    while (i > j)
                    {
                        if (Tile[j].Text != "" && Tile[j + 1].Text == "")
                        {
                            Tile[j + 1].Text = Tile[j].Text;
                            Tile[j].Text = "";
                            bTileMove = true;

                            j += 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    j = temp;
                }
            }
        }
        private void MoveLeft()
        {
            bTileMove = false;
            GameOver();
            Left();
            for (int j = 1; j <= 13; j += 4)
            {
                for (int i = j; i < j + 3; i++)
                {
                    if (Tile[i].Text == Tile[i + 1].Text && Tile[i].Text != "")
                    {
                        nCurrentTile = Convert.ToInt32(Tile[i].Text);
                        nNextTile = Convert.ToInt32(Tile[i + 1].Text);
                        nCurrentTile += nNextTile;
                        Tile[i].Text = nCurrentTile.ToString();
                        Tile[i + 1].Text = "";
                        ChangeBgColor();
                        nScore += nCurrentTile;
                        Score.Text = nScore.ToString();
                        bTileMove = true;
                    }
                }
            }
            Left();
            RandomTile();
        }
        void Left()
        {
            for (int i = 1; i <= 13; i += 4)
            {
                for (int j = i; j < i + 4; j++)
                {
                    int temp = j;
                    while (j - i >= 1)
                    {
                        Console.WriteLine(i + " : " + j);
                        if (Tile[j].Text != "" && Tile[j - 1].Text == "")
                        {
                            Tile[j - 1].Text = Tile[j].Text;
                            Tile[j].Text = "";
                            bTileMove = true;
                            j -= 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    j = temp;
                }
            }
        }

        private void MoveDown()
        {
            bTileMove = false;
            GameOver();
            Down();
            for (int j = 13; j <= 16; j++)
            {
                for (int i = j; i >= j - 8; i -= 4)
                {
                    if (Tile[i].Text == Tile[i - 4].Text && Tile[i].Text != "")
                    {
                        nCurrentTile = Convert.ToInt32(Tile[i - 4].Text);
                        nNextTile = Convert.ToInt32(Tile[i].Text);
                        nCurrentTile += nNextTile;
                        Tile[i - 4].Text = nCurrentTile.ToString();
                        Tile[i].Text = "";
                        ChangeBgColor();
                        nScore += nCurrentTile;
                        Score.Text = nScore.ToString();
                        bTileMove = true;
                    }

                }
            }
            Down();
            RandomTile();
        }


        private void Down()
        {
            for (int i = 13; i <= 16; i++)
            {
                for (int j = i; j >= 1; j -= 4)
                {
                    int temp = j;
                    while (j + 4 <= 16)
                    {
                        if (Tile[j].Text != "" && Tile[j + 4].Text == "")
                        {
                            Tile[j + 4].Text = Tile[j].Text;
                            Tile[j].Text = "";
                            bTileMove = true;
                            j += 4;
                        }
                        else
                        {
                            j = 16;
                        }
                    }
                    j = temp;
                }
            }
        }

        private void Up()
        {
            for (int i = 5; i < 9; i++)
            {
                for (int j = i; j <= 16; j += 4)
                {
                    int temp = j;
                    while (j - 4 >= 1)
                    {
                        if (Tile[j].Text != "" && Tile[j - 4].Text == "")
                        {
                            Tile[j - 4].Text = Tile[j].Text;
                            Tile[j].Text = "";
                            bTileMove = true;
                            j -= 4;
                        }
                        else
                        {
                            j = 1;
                        }

                    }
                    j = temp;
                }
            }
        }


        private void MoveUp()
        {
            bTileMove = false;
            GameOver();
            Up();
            //คำนวณการบวก 
            for (int j = 1; j < 12; j += 4)
            {
                for (int i = j; i < j + 4; i++)
                {
                    if (Tile[i].Text == Tile[i + 4].Text && Tile[i].Text != "")
                    {
                        nCurrentTile = Convert.ToInt32(Tile[i].Text);
                        nNextTile = Convert.ToInt32(Tile[i + 4].Text);
                        nCurrentTile += nNextTile;
                        Tile[i].Text = nCurrentTile.ToString();
                        Tile[i + 4].Text = "";
                        ChangeBgColor();
                        nScore += nCurrentTile;
                        Score.Text = nScore.ToString();
                        bTileMove = true;
                    }
                }
            }
            Up();
            RandomTile();
        }



        void GameOver()
        {
            bGameOver = true;
            for (int i = 1; i <= 16; i++)
            {
                if (Tile[i].Text == "")
                {
                    bGameOver = false;
                }

            }

            if (bGameOver)
            {
                //เเนวตั้ง
                Console.WriteLine("nospace");
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = i; j <= 12; j += 4)
                    {
                        if (Tile[j].Text == Tile[j + 4].Text)
                        {
                            bGameOver = false;
                        }
                    }
                }
                if (bGameOver)
                {
                    Console.WriteLine("nocol");
                    for (int i = 1; i <= 13; i += 4)
                    {
                        for (int j = i; j < i + 3; j++)
                        {
                            if (Tile[j].Text == Tile[j + 1].Text)
                            {
                                bGameOver = false;
                            }
                        }
                    }
                }

                /*StreamWriter writer = new StreamWriter(@"DataFile/Highscore.txt");
                if (nScore > Convert.ToInt32(Best))
                {
                    string s = Score.ToString();
                    writer.Write(Score.Text);
                    MessageBox.Show("ยินดีด้วยคุณ " + Lobby.sName + " ได้สกอร์ใหม่สูงสุด");
                }
                writer.Close();
                StreamWriter writer1 = new StreamWriter(@"DataFile/User.txt");
                name = Lobby.sName;
                string outp = Encipher(name, 13);
                writer1.Write(outp);
                writer.Close();*/
                /*return;*/
            }
            if (bGameOver)
            {
                MessageBox.Show("GameOver");
                buttonRestart.Show();
            }
        }
        void ChangeBgColor()
        {
            for (int i = 1; i <= 16; i++)
            {

                if (Tile[i].Text == "0" || Tile[i].Text == "")
                {
                    Tile[i].BackColor = Color.FromArgb(205, 193, 180);
                    Tile[i].Text = "";
                }

                if (Tile[i].Text == "2")
                {
                    Tile[i].ForeColor = Color.FromArgb(61, 57, 54);
                    Tile[i].BackColor = Color.FromArgb(204, 194, 184);
                    Tile[i].Font = new Font("ZoodHardSell2", 80, FontStyle.Bold);
                    Tile[i].Text = "2";
                }
                if (Tile[i].Text == "4")
                {
                    Tile[i].ForeColor = Color.FromArgb(61, 57, 54);
                    Tile[i].BackColor = Color.FromArgb(204, 184, 163);
                    Tile[i].Font = new Font("ZoodHardSell2", 90, FontStyle.Bold);
                    Tile[i].Text = "4";
                }
                if (Tile[i].Text == "8")
                {
                    Tile[i].ForeColor = Color.FromArgb(61, 57, 54);
                    Tile[i].BackColor = Color.FromArgb(204, 165, 122);
                    Tile[i].Text = "8";
                    Tile[i].Font = new Font("ZoodHardSell2", 80, FontStyle.Bold);
                }
                if (Tile[i].Text == "16")
                {

                    Tile[i].BackColor = Color.FromArgb(204, 135, 61);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 60, FontStyle.Bold);
                    Tile[i].Text = "16";

                }
                if (Tile[i].Text == "32")
                {

                    Tile[i].BackColor = Color.FromArgb(204, 115, 20);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 60, FontStyle.Bold);
                    Tile[i].Text = "32";

                }
                if (Tile[i].Text == "64")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 150, 122);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 60, FontStyle.Bold);
                    Tile[i].Text = "64";
                }
                if (Tile[i].Text == "128")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 109, 61);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 40, FontStyle.Bold);
                    Tile[i].Text = "128";
                }
                if (Tile[i].Text == "256")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 81, 20);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 40, FontStyle.Bold);
                    Tile[i].Text = "256";
                }
                if (Tile[i].Text == "512")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 122, 122);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 40, FontStyle.Bold);
                    Tile[i].Text = "512";
                }
                if (Tile[i].Text == "1024")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 61, 61);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 20, FontStyle.Bold);
                    Tile[i].Text = "1024";
                }
                if (Tile[i].Text == "2048")
                {
                    Tile[i].BackColor = Color.FromArgb(204, 20, 20);
                    Tile[i].ForeColor = Color.FromArgb(255, 255, 255);
                    Tile[i].Font = new Font("ZoodHardSell2", 20, FontStyle.Bold);
                    Tile[i].Text = "2048";
                }
            }
        }
    }
}
