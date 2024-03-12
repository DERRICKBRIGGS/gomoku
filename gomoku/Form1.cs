using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gomoku
{
    public partial class form1 : Form
    {
        Game game = new Game(); 
        public form1()
        {
            InitializeComponent();
        }

        private void form1_MouseDown(object sender, MouseEventArgs e)
        {
            //檢查是否能下棋子在正確位置
            piece newpiece = game.pieceCando(e.X, e.Y);
           // piece computer =game.computerWhite(e.X, e.Y);
            if (newpiece != null) 
            {
                this.Controls.Add(newpiece);
               // this.Controls.Add(computer);
            }
            if (game.victoryTips) 
            {
                game.victoryTips=false;
                if(game.winnerType == ColorType.Null)
                    MessageBox.Show("注意出現四子連線了");
            }
            if (game.winnerType == ColorType.Black)
            {
                MessageBox.Show($"黑棋獲勝");
                this.Controls.Clear();
                game.regame();
                game = new Game();
            }
            else if (game.winnerType == ColorType.White) 
            {
                MessageBox.Show("白棋獲勝");
                this.Controls.Clear();
                game.regame();
                game = new Game();
            }
        }

        private void form1_MouseMove(object sender, MouseEventArgs e)
        {
            bool cando = game.pieceTure(e.X, e.Y);
            if (cando)
            {
                this.Cursor = Cursors.Hand;
            }
            else 
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
