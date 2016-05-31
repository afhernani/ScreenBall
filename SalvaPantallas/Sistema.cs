using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalvaPantallas
{
    class Sistema
    {
        private Spot[] m_spot; //una matrip de spot
        private int m_t; //total de sport
        private int m_ancho, m_alto;
        private int m_dist;

        public Sistema(int ancho, int alto)
        {
            m_ancho = ancho;
            m_alto = alto;
            m_t = 5;
            InitSpot();
        }
        public Sistema(int t, int ancho, int alto)
        {
            m_ancho = ancho;
            m_alto = alto;
            m_t = t;
            m_dist = 24;
            InitSpot();

        }
        public Sistema(int t, int ancho, int alto, int dist)
        {
            m_ancho = ancho;
            m_alto = alto;
            m_t = t;
            m_dist = dist;
            InitSpot();

        }
        private void InitSpot()
        {
            //inicializa la matriz
            m_spot = new Spot[m_t];
            for (int i = 0; i < m_spot.Length; i++)
            {
                Spot obj = new Spot(new PointF(i*m_dist, i*m_dist),new PointF(10,5));
                obj.Voumen = m_dist;
                m_spot[i] = obj;
            }
        }
        public void OnRender(Graphics g)
        {
            for (int i = 0; i <m_spot.Length; i++)
            {
                m_spot[i].OnRender(g);
            }
        }
        public void move()
        {
            for (int i = 0; i < m_spot.Length; i++)
            {
                m_spot[i].move();
                
                for (int j = 0; j < m_spot.Length; j++)
                {
                    if (i != j)
                    {
                        //if (j< i)
                        //{
                            if (m_spot[i].Dohit(m_spot[j].Location))
                                m_spot[i].doCrash(m_spot[j]);
                        //}
                        //else
                        //{
                            //if (m_spot[i].Dohit(m_spot[j].nextMove()))
                            //    m_spot[i].doCrash(m_spot[j]);
                        //}  
                    }
                }

                m_spot[i].doLimit(m_ancho, m_alto);
            }
        }
    }
}
