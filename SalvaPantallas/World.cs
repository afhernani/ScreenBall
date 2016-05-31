using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalvaPantallas
{
    public partial class World : Form
    {
        //atributos
        
        Sistema sis;

        public World()
        {
            InitializeComponent();
            //fondo negro
            this.BackColor = Color.FromArgb(0, 0, 0);
            //formulario maximizado
            this.WindowState = FormWindowState.Maximized;
            //sin bordes ni nada
            this.FormBorderStyle = FormBorderStyle.None;
            //el formulario resive las acciones del teclado
            this.KeyPreview = true;
            //formulario siempre en primer plano
            this.TopLevel = true;
            //manejo evento del raton.
            this.Click += new EventHandler(World_Click);
            //manejo evento de teclado
            this.KeyPress += new KeyPressEventHandler(World_KeyPress);
            this.Paint += new PaintEventHandler(World_Paint);
            
            //prueba sistema
            
            //activa temporizador
            activaTemporizador();
        }

        private void World_Paint(object sender, PaintEventArgs e)
        {
                  
            Graphics g = e.Graphics;
            
            sis.OnRender(g);
        }

        private void World_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Escape)
                closeWorld();
        }

        private void World_Click(object sender, EventArgs e)
        {
            closeWorld();
        }

        private void closeWorld()
        {
            this.Close();
        }

        #region temporizador

        Timer temporizador;
        private void activaTemporizador()
        {
            temporizador = new Timer();
            temporizador.Tick += new EventHandler(temporizador_Tick);
            temporizador.Interval = 10;
            temporizador.Enabled = true;
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            sis.move();
            //movingObjectInWorld();
            Refresh();
        }

        #endregion temporizador

        private void World_ClientSizeChanged(object sender, EventArgs e)
        {
            sis = new Sistema(200, Width, Height,12);
        }
    }
}
