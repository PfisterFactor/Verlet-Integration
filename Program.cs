using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using VerletIntegration.Shapes;
using VerletIntegration.Constraints;
using VerletIntegration.Utility;
namespace VerletIntegration
{
    class Program
    {
        public static RenderWindow _window
        {
            get;
            private set;
        }

        static BoundsConstraint windowBounds;
        static double leftoverDelta = 0;
        static Clock deltaClock;

        public static bool mouseClick = false;
        public static Vector2f mousePos = new Vector2f(0, 0);
        public static double delta;
        public static int timesteps;
        public const int CONSTRAINTACCURACY = 3;
        public static bool gameRunning = true;

        static Rectangle Test;
        static Triangle Test2;
        


        static void Draw()
        {
            foreach (DrawableObject da in DrawableObject.activeObjects)
            {
                
                da.draw(_window);
            }

            _window.Display();
        }
        static void moveTowardsMouse(PointMass point)
        {
            
            
            double clampedMouseX = mousePos.X.Clamp(windowBounds.Size.X, windowBounds.Size.X + windowBounds.Size.Width);
            double clampedMouseY = mousePos.Y.Clamp(windowBounds.Size.Y, windowBounds.Size.Y + windowBounds.Size.Height);
            point.Acceleration.X += (float)clampedMouseX - point.Position.X;
            point.Acceleration.Y += (float)clampedMouseY - point.Position.Y;
        }
        static void Update()
        {

            double mDelta = deltaClock.ElapsedTime.AsMilliseconds();
            delta = deltaClock.ElapsedTime.AsSeconds();
            deltaClock.Restart();
            mDelta += leftoverDelta;

            timesteps = (int)(mDelta / 16);
            leftoverDelta = mDelta - timesteps * 16;

            _window.Clear(Color.White);
            _window.DispatchEvents();




            for (int i = 0; i < timesteps; i++)
            {
                
                if (mouseClick)
                {
                    Random a = new Random();
                    foreach (PointMass p in PointMass.activeObjects)
                    {
                        if (a.Next(0,2) == 1) continue;
                        moveTowardsMouse(p);
                        
                    }
                    
                }

                Console.WriteLine(Test.intersecting(Test2,_window));
                for (int iteration = 0; iteration < CONSTRAINTACCURACY; iteration++)
                {
                    foreach (Constraint C in Constraint.activeObjects)
                    {
                        C.solve();
                    }
                }

                foreach (PointMass P in PointMass.activeObjects)
                {
                    P.update(delta);
                }


            }

        }

        //TODO Implement shape classes (Rectangle done)
        static void Initialize()
        {
            deltaClock = new Clock();


            _window.MouseButtonPressed += Input.onMousePressed;
            _window.MouseButtonReleased += Input.onMouseRelease;
            _window.MouseMoved += Input.onMouseMove;
            _window.Closed += Input.onWindowClose;

            deltaClock.Restart();

            windowBounds = new BoundsConstraint(new Rect(0, 0, 800, 600),0.3f);

            Vector2f[] rect = { new Vector2f(25, 25), new Vector2f(125, 25), new Vector2f(125,125), new Vector2f(25,125)};
            Vector2f[] triangle = { new Vector2f(25, 25), new Vector2f(25, 125), new Vector2f(125, 125) };
            Test = new Rectangle(rect.Select(x => new PointMass(x)).ToArray());
            Test2 = new Triangle(triangle.Select(x => new PointMass(x)).ToArray());
            

            //TODO Attempt multithreading constraint solving

            /*
            PointMass TestOne = new PointMass(new Vector2f(25, 25));
            PointMass TestTwo = new PointMass(new Vector2f(125, 25));
            PointMass TestThree = new PointMass(new Vector2f(125, 125));
            PointMass TestFour = new PointMass(new Vector2f(25, 125));


            PointMass TestThree = new PointMass(new Vector2f(225, 200));

            PointMass TestFour = new PointMass(new Vector2f(150, 400));
            PointMass TestFive = new PointMass(new Vector2f(300, 400));
            PointMass TestSix = new PointMass(new Vector2f(225, 200));
            PointMass TestSeven = new PointMass(mousePos);

            
            RodConstraint TestRod2 = new RodConstraint(TestTwo, TestThree, 100);
            RodConstraint TestRod3 = new RodConstraint(TestThree, TestOne, 100);
            RodConstraint TestRod4 = new RodConstraint(TestFour, TestFive, 50);
            RodConstraint TestRod5 = new RodConstraint(TestFive, TestSix, 50);
            RodConstraint TestRod6 = new RodConstraint(TestSix, TestFour, 50);
            RodConstraint TestRod7 = new RodConstraint(TestSeven, TestOne, 50);
            

            RodConstraint TestRod = new RodConstraint(TestOne, TestTwo, 100);
            RodConstraint TestRod2 = new RodConstraint(TestTwo, TestThree, 100);
            RodConstraint TestRod3 = new RodConstraint(TestThree, TestFour, 100);
            RodConstraint TestRod4 = new RodConstraint(TestFour, TestOne, 100);
            double hypotenuse = Math.Sqrt((100 * 100) * 2);
            RodConstraint TestRod5 = new RodConstraint(TestOne, TestThree, hypotenuse);
            RodConstraint TestRod6 = new RodConstraint(TestFour, TestTwo, hypotenuse);
            */

            Constraint.drawAll(true);
            //PointMass.drawAll(true);



        }
        static void Main(string[] args)
        {
            _window = new RenderWindow(new VideoMode(800, 700), "Verlet Integration",Styles.Close);
            _window.SetVerticalSyncEnabled(true);

            Initialize();
            while (gameRunning)
            {
                
                Update();
                Draw();

            }

            _window.Close();
            
        }
    }
}
