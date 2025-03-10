using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace hogehoge
{
    public class MainForm : Form
    {
        //PictView_Test0から流用して全面改修
        public MainForm()
        {
            Controls.Add(MenuBar());
            Controls.Add(pictureView());
        }

        private MenuStrip MenuBar()
        {
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem menuFile = new ToolStripMenuItem("ファイル(&F)");
            menuStrip.Items.Add(menuFile);

            //ファイルのドロップダウンメニューに開くを追加
            ToolStripMenuItem menuFileOpen = new ToolStripMenuItem("開く(&O)");
            menuFile.DropDownItems.Add(menuFileOpen);
            menuFileOpen.Click += new EventHandler(Open_Click);

            //ファイルのドロップダウンメニューに終了を追加
            ToolStripMenuItem menuFileEnd = new ToolStripMenuItem("終了(&X)");
            menuFile.DropDownItems.Add(menuFileEnd);
            
            return menuStrip;
        }

        //ファイルを開く
        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //読み込み許可ファイルの種類の設定 
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png)|*.bmp;*.jpg;*.png|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            //ファイルを選択してOKしたときの処理
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine($"選択ファイル:{ofd.FileName}");
                string folderPath = Path.GetDirectoryName(ofd.FileName);
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath).Where(str => str.EndsWith(".bmp") || str.EndsWith(".jpg") || str.EndsWith(".png"));

                //フォルダ内部のファイルを全て読み込んでリストに格納
                List<string> filesList = files.ToList();

                //デバッグ用出力(読み込みファイル)
                foreach (string str in files)
                {
                    Console.WriteLine($"読み込みファイル:{str}");
                }
                //デバッグ用出力（読み込みファイル数）
                Console.WriteLine($"読み込みファイル数:{filesList.Count}");

                
                //リスト内をソート処理
                var sortQuery = filesList.OrderBy(s => s.Length);
                filesList = sortQuery.ToList();
                int loadFileNum = filesList.IndexOf(ofd.FileName);
                //ソート後のリスト内を出力
                filesList.ForEach(Console.WriteLine);

                //ファイル読み込みフラグオン
                //Flags.fileFlag |= Flags.fileFlags.FILE_LOAD;
                //Console.WriteLine($"{Flags.fileFlag}");


                //「画像読み込み」の実行
                //LoadImage();
            }

            Console.WriteLine("開く処理終了");

        }

        private PictureBox pictureView()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.Blue;
            return pictureBox;
        }

    }
}
