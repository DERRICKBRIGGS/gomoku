using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gomoku
{
    internal class Game
    {
        Bord Bord = new Bord();
        Random crandom = new Random();
        private ColorType currentPlayer = ColorType.Black;
        public ColorType winnerType = ColorType.Null;
        public bool victoryTips=false;
        public void regame() 
        {
            Bord.clearboard();
            victoryTips = false;
        }
        public piece computerWhite(int x,int y)
        //電腦下棋
        //使用點擊處周圍4*4周圍產生隨機棋子，如果無法生成，在四周可生成處產生
        {
            int ranx = crandom.Next(-2, 2)*Bord.DISTANCE;
            int rany = crandom.Next(-2, 2)* Bord.DISTANCE;
            int whiteX= x+ ranx;
            int whiteY= y+ rany;
            piece newpiece = Bord.pieceCando(whiteX, whiteY, currentPlayer);
            currentPlayer = ColorType.White;
            if (newpiece != null)
                return newpiece;
            else 
            {
                for (int dirX = -1; dirX <= 1; dirX++)
                    for (int dirY = -1; dirY <= 1; dirY++) 
                    {
                        if (dirX == 0 && dirY == 0)
                            continue;
                        int reX = x + Bord.DISTANCE * dirX;
                        int reY = y + Bord.DISTANCE * dirY;
                        piece center = Bord.pieceCando(reX, reY, currentPlayer);
                        if (center != null) return center;
                    }
            return null;
            }
        }
        public bool pieceTure(int x, int y)
        {
            return Bord.pieceTure(x, y);
        }
        public piece pieceCando(int x, int y)
        {
            //if (currentPlayer == ColorType.White)  //有電腦參與下棋不用換色(1)
            //    currentPlayer = ColorType.Black;
            piece newpiece = Bord.pieceCando(x, y, currentPlayer);
            if (newpiece != null)
            {
                chickWinner();
                //透過enum切換顏色，交換選手
                if (currentPlayer == ColorType.Black) 
                    currentPlayer= ColorType.White;
                else if (currentPlayer == ColorType.White) //無電腦時需要換色(1)
                    currentPlayer = ColorType.Black; 
                return newpiece;
            }
            return null;
        }
        public void chickWinner()
        {
            int centerX = Bord.LastPlaceNode.X;
            int centerY = Bord.LastPlaceNode.Y;
            //count是下的棋方向有幾顆同樣顏色的棋子
            //int count = 1;
            //八個方向的不同變數存小矩陣內有幾顆同色子
            int straightUp = 0;
            int straightDown = 0;
            int horizontalL = 0;
            int horizontalR = 0;
            int leftObliqueUp = 0;
            int leftObliqueDowm = 0;
            int rightObliqueUp = 0;
            int rightObliqueDown = 0;
            //如果檢查子超出邊界或遇到不同顏色則中斷
            //(centerX,centerY)為目前棋子的位子
            //(SquareX,SquareY)為判斷延伸的位子

            //檢查八個方向，各三顆在8*8方格中，找到同色棋
            //在下的這顆棋子附近8*8內有否連線(下的這顆是中間顆)
            for (int dirX = -1; dirX <= 1; dirX++) 
            {
                for (int dirY = -1; dirY <= 1; dirY++) 
                {
                    for (int smallSquare = 1; smallSquare <= 4; smallSquare++) 
                    {
                        if (dirX == 0 && dirY == 0)
                            continue;
                        int SquareX = centerX + smallSquare * dirX;
                        int SquareY = centerY + smallSquare * dirY;
                        if (SquareX < 0 || SquareX >= Bord.NODE_COUNT ||
                            SquareY < 0 || SquareY >= Bord.NODE_COUNT ||
                            Bord.GetPiceType(SquareX, SquareY) != currentPlayer)
                            continue;
                        if (Bord.GetPiceType(SquareX, SquareY) == currentPlayer) 
                        {
                            if (dirX == 1 && dirY == 0)
                                horizontalR++;
                            else if (dirX == -1 && dirY == 0)
                                horizontalL++;
                            else if (dirX == 0 && dirY == -1)
                                straightDown++;
                            else if (dirX == 0 && dirY == 1)
                                straightUp++;
                            else if (dirX == -1 && dirY == 1)
                                rightObliqueUp++;
                            else if (dirX == 1 && dirY == -1)
                                rightObliqueDown++;
                            else if (dirX == 1 && dirY == 1)
                                leftObliqueUp++;
                            else if (dirX == -1 && dirY == -1)
                                leftObliqueDowm++;
                        }
                    }   
                }
            }
            if (horizontalR + horizontalL >= 4 ||
                straightDown + straightUp >= 4 ||
                rightObliqueUp + rightObliqueDown >= 4 ||
                leftObliqueUp + leftObliqueDowm >= 4)
                winnerType = currentPlayer;

            if (horizontalR == 3 || horizontalL == 3 ||
                straightDown == 3 || straightUp == 3 ||
                rightObliqueUp == 3 || rightObliqueDown == 3 ||
                leftObliqueUp == 3 || leftObliqueDowm == 3)
                victoryTips = true;
            /*
            //第二種情形，出現四顆相連的子(下的這顆是第五顆棋)(會出現BUG)
            for (int dirX = -1; dirX <= 1; dirX++)
            {
                for (int dirY = -1; dirY <= 1; dirY++)
                {
                    if (dirX == 0 && dirY == 0)
                        continue;
                    while (count < 5)
                    {
                        int targetX = centerX + count * dirX;
                        int targetY = centerY + count * dirY;
                        if (targetX < 0 || targetX >= Bord.NODE_COUNT ||
                            targetY < 0 || targetY >= Bord.NODE_COUNT ||
                            Bord.GetPiceType(targetX, targetY) != currentPlayer)
                            break;
                        count++;
                    }
                    if (count == 5)
                    {
                        winnerType = currentPlayer;
                        num5 = count;
                    }
                }
            }*/
        }
    }
}