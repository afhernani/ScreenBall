using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvaPantallas
{
    class Spot
    {
        //propiedades
        PointF location; //vector posicion
        PointF velocXY; //vector velocidad
        static float gravedad = 0.2f; //gravedad
        Color col; //color de relleno
        Random rand; //numero aleatorio
        int log = 24; //ancho recuadro
        Rectangle m_rect; //rectangulo de posicion
        //bool m_hit = false;
        //atributos
        public float Gravedad { set { gravedad = value; } }
        public PointF Velocidad { get { return velocXY; } set { velocXY = value; } }
        public PointF Location { get { return location; } set { location = value; } }
        public Rectangle Rectang { get { return m_rect; } }
        public int Voumen { set { log = value; } }
        //constructor.
        public Spot()
        {
            location = PointF.Empty;
            velocXY = PointF.Empty;
            rand = new Random(Environment.TickCount);
            col = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            m_rect = new Rectangle((int)location.X - log/2, (int)location.Y - log/2, log, log);
        }
        public Spot(PointF local, PointF velocidad)
        {
            location = local;
            velocXY = velocidad;
            rand = new Random(Environment.TickCount);
            col = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            m_rect = new Rectangle((int)location.X - log/2, (int)location.Y - log/2, log, log);
        }
        public PointF nextMove()
        {
            float x, y;
            float vx, vy;
            vx = velocXY.X;
            vy = velocXY.Y; vy += gravedad;
            x = location.X; y = location.Y;
            x += vx; y += vx;
            PointF p = new PointF(x, y);
            return p;
        }
        public Rectangle nextMoveRectangle()
        {
            PointF p = this.nextMove();
            return new Rectangle((int)p.X - log/2, (int)p.Y - log/2, log, log);
        }
        public void move()
        {
            velocXY.Y += gravedad;
            location.X += velocXY.X;
            location.Y += velocXY.Y;
            if (Math.Abs(velocXY.X) < 0.1 && Math.Abs(velocXY.Y) < 0.5 && DateTime.Now.Second % 3 == 0) // && Y == Height - 24)
            {
                Bounce();
            }
            //m_rect = new Rectangle((int)location.X - log / 2, (int)location.Y - log / 2, log, log);
        }
        public virtual void OnRender(Graphics g)
        {
            m_rect = new Rectangle((int)location.X - log / 2, (int)location.Y - log / 2, log, log);
            g.FillPie(new SolidBrush(col), (int)location.X, (int)location.Y, log, log, 0, 360);
            g.DrawArc(new Pen(Color.Black, 2), (int)location.X, (int)location.Y, log, log, 0, 360);
        }
        private void DoBounce()
        {
            velocXY.X = (float)(rand.NextDouble() + rand.NextDouble()) - 1;
            velocXY.Y = -(float)(rand.NextDouble());
            velocXY.X *= 20;
            velocXY.Y *= 20;
            location.X += velocXY.X;
            location.Y += velocXY.Y;
        }
        private void Bounce()
        {
            velocXY.X =(float) (rand.NextDouble() + rand.NextDouble()) - 1;
            velocXY.Y = -(float)(rand.NextDouble());
            velocXY.X *= 50;
            velocXY.Y *= 50;
            location.X += velocXY.X;
            location.Y += velocXY.Y;
            //m_rect = new Rectangle((int)location.X - log / 2, (int)location.Y - log / 2, log, log);
        }
        public bool Dohit(PointF loca)
        {
            return m_rect.Contains((int)loca.X, (int)loca.Y);
        }
        public bool Dohit(Rectangle rect)
        {
            return m_rect.Contains(rect);
        }
        public void doCrash(Spot left)
        {
            DoBounce();
            left.DoBounce();
        }
        public void doLimit(int ancho, int alto)
        {
            //Chequea la colision de los limites del sistema
            if (location.X < 0)
            {
                location.X = 0;
                velocXY.X = -velocXY.X;// = -moveX;
                velocXY.X *= 0.75f;// moveX *= 0.75;
                velocXY.Y = 0.95f;//moveY *= 0.95;
            }
            if (location.X > ancho - log)
            {
                location.X = ancho - log;// X = Width - 24;
                velocXY.X = -velocXY.X;// moveX = -moveX;
                velocXY.X *= 0.75f;
                velocXY.Y *= 0.95f;
            }

            if (location.Y < 0)
            {
                location.Y = 0;
                velocXY.Y = -velocXY.Y;// moveY = -moveY;
                velocXY.Y *= 0.75f;// moveY *= 0.75;
                velocXY.X *= 0.95f;// moveX *= 0.95;
            }
            if (location.Y > alto - log)
            {
                location.Y = alto - log;
                velocXY.Y = -velocXY.Y;// moveY = -moveY;
                velocXY.Y *= 0.8f;// moveY *= 0.8;
                velocXY.X *= 0.95f;// moveX *= 0.95;
            }
        }

    }
}
