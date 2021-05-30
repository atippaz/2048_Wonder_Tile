﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048Tile
{
    public partial class WonderTile : Form
    {
        int nTime = 0, nValue, nPosition,nScore=0;
        int nBefore, nAfter; 
        bool bGameOver = false,bTileMove = false;
        readonly Random rdRandom = new Random();
        readonly Label[] Tile = new Label[17];
        public WonderTile()
        {
            InitializeComponent();
            //สร้างแบบจำลองกระเบื้อง 16 อัน เพื่อนเทียบกับกระเบื้องจริง
            for (int i = 1; i <= 16; i++)
            {
                Tile[i] = (Label)this.Controls.Find("tile" + i, true)[0];
            }
           
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimumBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //เมื่อเวลาเปลี่ยนไป

        //สุ่มค่ากะเบื้อง  2 - 4 โดยโอกาสอยู่ที่ 2 = 0.6% 4 = 0.4% 
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
        private void NewGame_Click(object sender, EventArgs e)
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
            if (e.KeyCode == Keys.W)
            {
                MoveUp();
            }

            if (e.KeyCode == Keys.S)
            {
                MoveDown();
            }
            if (e.KeyCode == Keys.A)
            {
                MoveLeft();
            }
            if (e.KeyCode == Keys.D)
            {
                MoveRight();
            }
            if (e.KeyCode == Keys.Up)
            {
                MoveUp();
            }
            if (e.KeyCode == Keys.Left)
            {
                MoveLeft();
            }
            if (e.KeyCode == Keys.Down)
            {
                MoveDown();
            }
            if (e.KeyCode == Keys.Right)
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
            throw new NotImplementedException();
        }

        private void MoveLeft()
        {
            throw new NotImplementedException();
        }

        private void MoveDown()
        {
            bTileMove = false;
            GameOver();                          
            for (int j = 13; j <= 16; j++)
            {
                for (int i = j; i > 4; i -= 4)
                {
                    if (Tile[i - 4].Text == Tile[i].Text)
                    {
                        if (Tile[i].Text != "")
                        {
                            int nBefore = Convert.ToInt32(Tile[i - 4].Text);
                            int nAfter = Convert.ToInt32(Tile[i].Text);


                            nBefore = nBefore + nAfter;
                            Tile[i].Text = nBefore.ToString();
                            Tile[i - 4].Text = "";
                            ChangeBgColor();
                            nScore += nBefore;
                            Score.Text = nScore.ToString();
                            bTileMove = true;
                        }

                    }

                }
                for (int i = 5; i <= 8; i++)
                {

                    if ((Tile[i].Text != "") && Tile[i + 4].Text == "")
                    {
                        Tile[i + 4].Text = Tile[i].Text;
                        Tile[i].Text = "";
                        bTileMove = true;
                        if ((Tile[i + 4].Text == "") && Tile[i + 8].Text == "")
                        {
                            if (Tile[i + 4].Text != "")
                            {
                                int nBefore = Convert.ToInt32(Tile[i + 4].Text);
                                int nAfter = Convert.ToInt32(Tile[i + 8].Text);


                                nBefore = nBefore + nAfter;
                                Tile[i + 8].Text = nBefore.ToString();
                                Tile[i + 4].Text = "";
                                nScore += nBefore;
                                Score.Text = nScore.ToString();
                            }
                        }
                        ChangeBgColor();
                    }

                }
                for (int i = 1; i <= 4; i++)
                {

                    if ((Tile[i].Text != "") && Tile[i + 4].Text == "")
                    {
                        Tile[i + 4].Text = Tile[i].Text;
                        Tile[i].Text = "";
                        bTileMove = true;
                        ChangeBgColor();
                    }

                }
            }
           
               RandomTile();
            
        }

        private void MoveUp()
        {
            bTileMove = false;
            GameOver();

            /* for (int i = 1; i <= 4; i++)
             {
                 if (Tile[i].Text == Tile[i + 4].Text)
                 {
                     if (Tile[i].Text != "")
                     {
                         int nCurrentTile = Convert.ToInt32(Tile[i].Text);
                         int nNextTile = Convert.ToInt32(Tile[i + 4].Text);
                         nCurrentTile += nNextTile;
                         Tile[i].Text = nCurrentTile.ToString();
                         Tile[i + 4].Text = "";
                         ChangeBgColor();
                         nScore += nCurrentTile;
                         Score.Text = nScore.ToString();
                         bTileMove = true;
                     }
                 }
                 if (Tile[i + 4].Text == Tile[i + 8].Text)
                 {
                     if (Tile[i + 4].Text != "")
                     {
                         int nCurrentTile = Convert.ToInt32(Tile[i + 4].Text);
                         int nNextTile = Convert.ToInt32(Tile[i + 8].Text);
                         nCurrentTile += nNextTile;
                         Tile[i + 4].Text = nCurrentTile.ToString();
                         Tile[i + 8].Text = "";
                         ChangeBgColor();
                         nScore += nCurrentTile;
                         Score.Text = nScore.ToString();
                         bTileMove = true;
                     }
                 }
                 else if (Tile[i].Text == Tile[i + 8].Text)
                 {
                     if (Tile[i].Text != "" || Tile[i].Text != "0")
                     {
                         int nCurrentTile = Convert.ToInt32(Tile[i].Text);
                         int nNextTile = Convert.ToInt32(Tile[i + 8].Text);
                         nCurrentTile = nCurrentTile + nNextTile;
                         Tile[i].Text = nCurrentTile.ToString();
                         Tile[i + 8].Text = "0";
                         ChangeBgColor();
                         nScore += nCurrentTile;
                         Score.Text = nScore.ToString();
                     }
                 }

                 if (Tile[i + 8].Text == Tile[i + 12].Text)
                 {
                     if (Tile[i + 8].Text != "")
                     {
                         int nCurrentTile = Convert.ToInt32(Tile[i + 8].Text);
                         int nNextTile = Convert.ToInt32(Tile[i + 12].Text);
                         nCurrentTile += nNextTile;
                         Tile[i + 8].Text = nCurrentTile.ToString();
                         Tile[i + 12].Text = "0";
                         ChangeBgColor();
                         nScore += nCurrentTile;
                         Score.Text = nScore.ToString();
                         bTileMove = true;
                     }
                 }*/
            for (int j = 1; j <= 4; j++)
            {
                for (int i = j; i <= 12; i += 4)
                {
                    if (Tile[i + 4].Text == Tile[i].Text)
                    {
                        if (Tile[i].Text != "")
                        {
                            int nBefore = Convert.ToInt32(Tile[i + 4].Text);
                            int nAfter = Convert.ToInt32(Tile[i].Text);


                            nBefore = nBefore + nAfter;
                            Tile[i].Text = nBefore.ToString();
                            Tile[i + 4].Text = "";
                            ChangeBgColor();
                            nScore += nBefore;
                            Score.Text = nScore.ToString();
                            bTileMove = true;
                        }

                    }

                }
                for (int i = 9; i <= 12; i++)
                {

                    if ((Tile[i].Text != "") && Tile[i - 4].Text == "")
                    {
                        Tile[i - 4].Text = Tile[i].Text;
                        Tile[i].Text = "";
                        bTileMove = true;
                        if ((Tile[i - 4].Text == "") && Tile[i - 8].Text == "")
                        {
                            if (Tile[i - 4].Text != "")
                            {
                                int nBefore = Convert.ToInt32(Tile[i - 4].Text);
                                int nAfter = Convert.ToInt32(Tile[i - 8].Text);


                                nBefore = nBefore + nAfter;
                                Tile[i - 4].Text = "";
                                nScore += nBefore;
                                Score.Text = nScore.ToString();
                            }
                        }
                        ChangeBgColor();
                    }

                }
                for (int i = 13; i <= 16; i++)
                {

                    if ((Tile[i].Text != "") && Tile[i - 4].Text == "")
                    {
                        Tile[i - 4].Text = Tile[i].Text;
                        Tile[i].Text = "";
                        bTileMove = true;
                        ChangeBgColor();
                    }

                }
            }
            //}
            /*for (int i = 1; i <= 4; i++)
            {
                if (Tile[i].Text != "")
                {
                    Tile[i].Text = Tile[i + 4].Text;
                    Tile[i + 4].Text = "";
                    ChangeBgColor();
                }
            }
            for (int i = 9; i <= 12; i++)
            {
                if (Tile[i-8].Text == "")
                {
                    if (Tile[i - 4].Text == "")
                    {
                        Tile[i - 8].Text = Tile[i].Text;
                        Tile[i].Text = "";
                        ChangeBgColor();
                    }
                    else if (Tile[i - 4].Text != "")
                    {
                        if (Tile[i - 4].Text == Tile[i].Text)
                        {
                            nBefore = Convert.ToInt32(Tile[i].Text);
                            nAfter = Convert.ToInt32(Tile[i - 8].Text);
                            nBefore += nAfter;
                            Tile[i - 4].Text = nBefore.ToString();
                            Tile[i].Text = "";
                            ChangeBgColor();
                        }  
                    } 
                }
                else if(Tile[i - 8].Text != "")
                {
                    if (Tile[i - 4].Text == "")
                    {
                        if (Tile[i].Text == Tile[i - 8].Text)
                        {
                            nBefore = Convert.ToInt32(Tile[i].Text);
                            nAfter = Convert.ToInt32(Tile[i - 8].Text);
                            nBefore += nAfter;
                            Tile[i - 8].Text = Tile[i].Text;
                            Tile[i].Text = "";
                            ChangeBgColor();
                        }
                        else
                        {
                            
                        }
                    }
                }
            }*/
            RandomTile();
            
        }

        private void Up()
        {
            for (int i = 13; i <= 16; i++)
            {
                for (int j = i; j > 4; j -= 4)
                {
                    if ((Tile[j].Text == "") && Tile[j - 4].Text != "")
                    {
                        Tile[j-4].Text = Tile[j].Text;
                        Tile[j].Text = "";
                        bTileMove = true;
                        ChangeBgColor();
                    }
                }  
            }
        }

        void GameOver()
        {
            bGameOver = true;
            for (int u = 1; u <= 9; u += 4)
            {
                for (int i = u; i <= u+2; i++)
                {
                    if (Tile[i].Text == Tile[i + 1].Text || Tile[i].Text == Tile[i + 4].Text)
                    {
                        bGameOver = false;
                    }
                }
                if (Tile[u+3].Text == Tile[u + 7].Text)
                {
                    bGameOver = false;
                }
            }
            if (bGameOver == true)
            {
                MessageBox.Show("GameOver");
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
                return;
            }
        }
        void ChangeBgColor()
        {
            for (int i = 1; i <= 16; i++)
            {

                if (Tile[i].Text == "0"|| Tile[i].Text == "")
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
                    Tile[i].Font = new Font("ZoodHardSell2", 80, FontStyle.Bold);
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
