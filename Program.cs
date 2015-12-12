using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
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
        

        public static void onWindowClose (object sender, EventArgs e)
        {
            gameRunning = false;
        }
        public static void onMousePressed (object sender, EventArgs e)
        {
            mouseClick = true;
            
        }
        public static void onMouseRelease (object sender, EventArgs e)
        {
            mouseClick = false;
        }
        public static void onMouseMove (object sender, MouseMoveEventArgs e)
        {
                mousePos = new Vector2f(e.X, e.Y);
        }
        static void Draw()
        {
            foreach (DrawableObject da in DrawableObject.activeObjects)
            {
                
                da.draw(_window);
            }
            _window.Display();
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
                    PointMass.activeObjects[0].Position.X = mousePos.X.Clamp(windowBounds.Size.X, windowBounds.Size.X + windowBounds.Size.Width);
                    PointMass.activeObjects[0].Position.Y = mousePos.Y.Clamp(windowBounds.Size.Y, windowBounds.Size.Y + windowBounds.Size.Height);
                    PointMass.activeObjects[0].LastPosition = PointMass.activeObjects[0].Position;
                }

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

        //TODO Implement shape classes
        static void Initialize()
        {
            deltaClock = new Clock();


            _window.MouseButtonPressed += onMousePressed;
            _window.MouseButtonReleased += onMouseRelease;
            _window.MouseMoved += onMouseMove;
            _window.Closed += onWindowClose;

            deltaClock.Restart();

            windowBounds = new BoundsConstraint(new Rect(0, 0, 800, 600),0.3f);

            PointMass TestOne = new PointMass(new Vector2f(0, 400));
            PointMass TestTwo = new PointMass(new Vector2f(100, 400));
            PointMass TestThree = new PointMass(new Vector2f(0, 0));
            PointMass TestFour = new PointMass(new Vector2f(100, 0));
            /*
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
            */
            RodConstraint TestRod = new RodConstraint(TestOne, TestTwo, 100);
            RodConstraint TestRod2 = new RodConstraint(TestTwo, TestThree, 100);
            RodConstraint TestRod3 = new RodConstraint(TestThree, TestFour, 100);
            RodConstraint TestRod4 = new RodConstraint(TestFour, TestOne, 100);
            RodConstraint TestRod5 = new RodConstraint(TestOne, TestThree, 141);
            RodConstraint TestRod6 = new RodConstraint(TestFour, TestTwo, 141);

            Constraint.drawAll(true);
            PointMass.drawAll(true);



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
