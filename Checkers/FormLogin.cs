using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public partial class FormLogin : Form
    {
        private bool m_BothPlayers = false;
        private int m_BoarsSize;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxName2.Enabled = !textBoxName2.Enabled;
            m_BothPlayers = !m_BothPlayers;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowGame windowGame;

            if (!m_BothPlayers)
            {
                windowGame = new WindowGame(textBoxName1.Text, null, m_BoarsSize);
            }
            else
            {
                windowGame = new WindowGame(textBoxName1.Text, textBoxName2.Text, m_BoarsSize);
            }
            
            windowGame.RunGame();
            this.Close();
        }

        private void radioButtonSize_Click(object sender, EventArgs e)
        {
            if (sender == radioButtonSize6)
            {
                m_BoarsSize = 6;
                radioButtonSize8.Checked = false;
                radioButtonSize10.Checked = false;
            }
            else if (sender == radioButtonSize8)
            {
                m_BoarsSize = 8;
                radioButtonSize6.Checked = false;
                radioButtonSize10.Checked = false;
            }
            else
            {
                m_BoarsSize = 10;
                radioButtonSize6.Checked = false;
                radioButtonSize8.Checked = false;
            }
        }
    }
}
