﻿using Meadow.Foundation;
using Meadow.Foundation.Graphics;
using Meadow.Graphics;
using System;
using System.Threading.Tasks;

namespace Meadow
{
    public class CubeApp : App<Simulation.SimulatedMeadow<Simulation.SimulatedPinout>>
    {
        MicroGraphics graphics;
        private Display display;

        //needs cleanup - quick port from c code
        double rot, rotationX, rotationY, rotationZ;
        double rotationXX, rotationYY, rotationZZ;
        double rotationXXX, rotationYYY, rotationZZZ;

        int[,] cubeWireframe = new int[12, 3];
        int[,] cubeVertices;

        public override async Task Run()
        {
            _ = Task.Run(() =>
            {
                Show3dCube();
            });
            display.Run();
        }

        public override Task Initialize()
        {
            display = new Meadow.Graphics.Display();

            int cubeSize = 100;

            cubeVertices = new int[8, 3] {
                 { -cubeSize, -cubeSize,  cubeSize},
                 {  cubeSize, -cubeSize,  cubeSize},
                 {  cubeSize,  cubeSize,  cubeSize},
                 { -cubeSize,  cubeSize,  cubeSize},
                 { -cubeSize, -cubeSize, -cubeSize},
                 {  cubeSize, -cubeSize, -cubeSize},
                 {  cubeSize,  cubeSize, -cubeSize},
                 { -cubeSize,  cubeSize, -cubeSize},
            };

            /*  cube_vertex = new int[8, 3] {
                   { -20, -20, front_depth},
                   {  20, -20, front_depth},
                   {  20,  20, front_depth},
                   { -20,  20, front_depth},
                   { -20, -20, back_depth},
                   {  20, -20, back_depth},
                   {  20,  20, back_depth},
                   { -20,  20, back_depth}
              };  */

            graphics = new MicroGraphics(display);

            return base.Initialize();
        }

        void Show3dCube()
        {
            int originX = (int)display.Width / 2;
            int originY = (int)display.Height / 2;

            int angle = 0;

            while (true)
            {
                InvokeOnMainThread((_) =>
                {
                    graphics.Clear();

                    angle++;
                    for (int i = 0; i < 8; i++)
                    {
                        rot = angle * 0.0174532; //0.0174532 = one degree
                                                 //rotateY

                        rotationZ = cubeVertices[i, 2] * Math.Cos(rot) - cubeVertices[i, 0] * Math.Sin(rot);
                        rotationX = cubeVertices[i, 2] * Math.Sin(rot) + cubeVertices[i, 0] * Math.Cos(rot);
                        rotationY = cubeVertices[i, 1];

                        //rotateX
                        rotationYY = rotationY * Math.Cos(rot) - rotationZ * Math.Sin(rot);
                        rotationZZ = rotationY * Math.Sin(rot) + rotationZ * Math.Cos(rot);
                        rotationXX = rotationX;
                        //rotateZ
                        rotationXXX = rotationXX * Math.Cos(rot) - rotationYY * Math.Sin(rot);
                        rotationYYY = rotationXX * Math.Sin(rot) + rotationYY * Math.Cos(rot);
                        rotationZZZ = rotationZZ;

                        //orthographic projection
                        rotationXXX = rotationXXX + originX;
                        rotationYYY = rotationYYY + originY;

                        //store new vertices values for wireframe drawing
                        cubeWireframe[i, 0] = (int)rotationXXX;
                        cubeWireframe[i, 1] = (int)rotationYYY;
                        cubeWireframe[i, 2] = (int)rotationZZZ;

                        DrawVertices();
                    }

                    DrawWireframe();

                    graphics.Show();

                    //                    Thread.Sleep(20);
                });
            }
        }

        void DrawVertices()
        {
            graphics.DrawPixel((int)rotationXXX, (int)rotationYYY);
        }

        void DrawWireframe()
        {
            graphics.DrawLine(cubeWireframe[0, 0], cubeWireframe[0, 1], cubeWireframe[1, 0], cubeWireframe[1, 1], Color.White);
            graphics.DrawLine(cubeWireframe[1, 0], cubeWireframe[1, 1], cubeWireframe[2, 0], cubeWireframe[2, 1], Color.White);
            graphics.DrawLine(cubeWireframe[2, 0], cubeWireframe[2, 1], cubeWireframe[3, 0], cubeWireframe[3, 1], Color.White);
            graphics.DrawLine(cubeWireframe[3, 0], cubeWireframe[3, 1], cubeWireframe[0, 0], cubeWireframe[0, 1], Color.White);

            //cross face above
            graphics.DrawLine(cubeWireframe[1, 0], cubeWireframe[1, 1], cubeWireframe[3, 0], cubeWireframe[3, 1], Color.White);
            graphics.DrawLine(cubeWireframe[0, 0], cubeWireframe[0, 1], cubeWireframe[2, 0], cubeWireframe[2, 1], Color.White);

            graphics.DrawLine(cubeWireframe[4, 0], cubeWireframe[4, 1], cubeWireframe[5, 0], cubeWireframe[5, 1], Color.White);
            graphics.DrawLine(cubeWireframe[5, 0], cubeWireframe[5, 1], cubeWireframe[6, 0], cubeWireframe[6, 1], Color.White);
            graphics.DrawLine(cubeWireframe[6, 0], cubeWireframe[6, 1], cubeWireframe[7, 0], cubeWireframe[7, 1], Color.White);
            graphics.DrawLine(cubeWireframe[7, 0], cubeWireframe[7, 1], cubeWireframe[4, 0], cubeWireframe[4, 1], Color.White);

            graphics.DrawLine(cubeWireframe[0, 0], cubeWireframe[0, 1], cubeWireframe[4, 0], cubeWireframe[4, 1], Color.White);
            graphics.DrawLine(cubeWireframe[1, 0], cubeWireframe[1, 1], cubeWireframe[5, 0], cubeWireframe[5, 1], Color.White);
            graphics.DrawLine(cubeWireframe[2, 0], cubeWireframe[2, 1], cubeWireframe[6, 0], cubeWireframe[6, 1], Color.White);
            graphics.DrawLine(cubeWireframe[3, 0], cubeWireframe[3, 1], cubeWireframe[7, 0], cubeWireframe[7, 1], Color.White);
        }
    }
}