using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;


namespace Sudoku
{
    public partial class Form1 : Form
    {
        Sudoku S;

        private SoundPlayer SquarePlace;
        private SoundPlayer PuzzleSolved;
        private SoundPlayer PuzzleFine;
        private SoundPlayer MoveNo;

        public Form1()
        {
            InitializeComponent();
            S = new Sudoku();
            Application.DoEvents();
            S.GenerateGame();
            LoadSounds();
            S.PlaySound += new Sudoku.SudokuEvent(S_PlaySound);
            S.RequestRepaint += new Sudoku.SudokuEvent2(S_RequestRepaint);
        }

        void S_RequestRepaint()
        {
            pictureBox1.Invalidate();
        }

        private void LoadSounds()
        {

            SquarePlace = new SoundPlayer("square.wav");
            PuzzleSolved = new SoundPlayer("warm-interlude.wav");
            PuzzleFine = new SoundPlayer("fine.wav");
            MoveNo = new SoundPlayer("no.wav");

            Application.DoEvents();
            /*
            SquarePlace.Load();
            PuzzleSolved.Load();
            PuzzleFine.Load();
            MoveNo.Load();
             * */
        }

        void S_PlaySound(Sudoku.SudokuSound S)
        {
            /*
            switch (S)
            {
                case Sudoku.SudokuSound.Square:
                   // SquarePlace.Play();
                    break;
                case Sudoku.SudokuSound.Solved:
                    PuzzleSolved.Play();
                    break;
                case Sudoku.SudokuSound.No:
                    MoveNo.Play();
                    break;
                case Sudoku.SudokuSound.Fine:
                    PuzzleFine.Play();
                    break;
                case Sudoku.SudokuSound.Stop :
                    PuzzleSolved.Stop();
                    break;
            }
             */
             

        }

        //스도쿠 형태 구현
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            S.Draw(e.Graphics, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
        
        //블록 선택
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                S.SetLocation(e.Location);

                pictureBox1.Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            S.KeyPress((char)e.KeyValue);

            if ((int)e.KeyValue == 27) S.Deselect();

            if ((int)e.KeyValue == 46) S.DeleteCurrentSquare();

            pictureBox1.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            S.GenerateGame();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            S.RenderMessage("Sudoku 1.0", "DongHyeon", false);

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(S.GetGameString());
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool result = S.SetGameString(Clipboard.GetText());

            if (!result)
            {
                Application.DoEvents();

                MessageBox.Show("The clipboard does not contain a valid puzzle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.checkSolvable(Sudoku.SolveMethods.All) == false)
            {
                Application.DoEvents();

                MessageBox.Show("퍼즐 오류 발생", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                S.SolvePuzzle(Sudoku.SolveMethods.All);

                pictureBox1.Invalidate();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            S.SolveStep(Sudoku.SolveMethods.All);

            pictureBox1.Invalidate();
        }

        //Progress 체크
        private void checkSolvabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.isSolved()) return;

            int error_count = S.ComputeErrors();

            if (error_count == 0)
            {
                S.RenderMessage("아무 문제 없습니다.", "아무키나 눌러 계속진행", false);

                pictureBox1.Invalidate();

                Application.DoEvents();

                //   PuzzleFine.Play();
            }
            else
            {
                Application.DoEvents();

                S.ShowErrors = true;

                pictureBox1.Invalidate();
            }

        }

        //파일(퍼즐) 불러오기
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            DialogResult R = openFileDialog1.ShowDialog();

            if (R != DialogResult.OK) return;

            bool result = S.LoadFile(openFileDialog1.FileName);

            if (!result)
            {
                Application.DoEvents();

                MessageBox.Show("파일 불러오기 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        //풀이 횟수 체크
        private void checkSolutionsCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (S.isSolved()) return;

            Sudoku.SolutionStepList L = S.ComputePossibleSteps(Sudoku.SolveMethods.All);

            S.RenderMessage(L.Count().ToString() + " 남은 기본 풀이 횟수", "아무키나 눌러 계속 진행", false);
        }

        //파일(퍼즐) 저장하기
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult R = saveFileDialog1.ShowDialog();

            if (R != DialogResult.OK) return;

            bool result = S.SaveFile(saveFileDialog1.FileName);

            if (!result)
            {
                Application.DoEvents();

                MessageBox.Show("파일 저장 오류", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            pictureBox1.Invalidate();
        }

        private void menuStrip1_MouseHover(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            pictureBox1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}