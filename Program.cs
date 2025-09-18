using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

class Program : ConsoleGameEngine {
	public Program(int screenwidth, int screenheight, int fps) : base(screenwidth, screenheight, fps) {}
	public static Program? game;

	public const int mapWidth = 9;
	public const int mapHeight = 9;

	public static readonly char[] map = {
	'#','#','#','#','#','#','#','#','#',
	'#',' ',' ',' ',' ',' ',' ',' ','#',
	'#',' ','#',' ',' ',' ','#',' ','#',
	'#',' ','#','#','#','#','#',' ','#',
	'#',' ',' ','#',' ',' ','#',' ','#',
	'#',' ',' ','#',' ',' ','#',' ','#',
	'#',' ',' ','#',' ',' ','#',' ','#',
	'#',' ',' ','#',' ',' ',' ',' ',' ',
	'#','#','#','#','#','#','#',' ',' ',
	};

	public const int tileSize = 32;

	double x = 36;
	double y = 36;

	double angle = 0;

	const double fov = Math.PI / 2;

	void castRays() {
		double currentAngle = angle - fov / 2;
		double angleStep = Math.PI / 2 / screenWidth;

		for (int i = 0; i < screenWidth; i++) {
			Ray ray = new Ray(x, y, currentAngle, 500);

			int lineHeight;

			if (ray.hitWall) {
				lineHeight = (int)((double)screenHeight / (double)ray.length * 10);
			}
			else {
				lineHeight = 0;
			}

			int lineY = screenHeight / 2 - lineHeight / 2;

			char type = ' ';

			if (ray.length < 20 && ray.length > 0) {
				type = FULL;
			}
			else if (ray.length < 40 && ray.length >= 20) {
				type = DARK;
			}
			else if (ray.length < 60 && ray.length >= 40) {
				type = MEDIUM;
			}
			else if (ray.length < 150 && ray.length >= 60) {
				type = LIGHT;
			}
			else if (ray.length >= 150) {
				type = ' ';
			}

			DrawVerticalLine(i, lineY, lineY + lineHeight, type);
			DrawVerticalLine(i, lineY + lineHeight, screenHeight, LIGHT);

			currentAngle += angleStep;
		}
	}

	public void DrawMap() {
		Console.SetCursorPosition(0, 0);

		for (int y1 = 0; y1 < mapHeight; y1++) {
			for (int x1 = 0; x1 < mapWidth; x1++) {
				Console.Write(map[y1 * mapWidth + x1]);

				if (((int)y - (int)y % tileSize) / tileSize * mapWidth + ((int)x - (int)x % tileSize) / tileSize == y1 * mapWidth + x1) {
					Console.SetCursorPosition(x1, y1);
					Console.Write("P");
				}
			}
			Console.Write("\n");
		}
	}

	public override void Update() {
		if (Console.KeyAvailable) {
			switch (Console.ReadKey(true).Key) {
				case ConsoleKey.Escape:
					End();
					break;
				case ConsoleKey.W:
					x += Math.Cos(angle);
					y += Math.Sin(angle);
					break;
				case ConsoleKey.S:
					x -= Math.Cos(angle);
					y -= Math.Sin(angle);
					break;
				case ConsoleKey.D:
					angle += 0.1 * Math.PI;
					break;
				case ConsoleKey.A:
					angle -= 0.1 * Math.PI;
					break;
			}
		}
	}

	public override void Draw() {
		ClearScreenBuffer();

		castRays();

		SwapBuffers();

		DrawMap();
	}

	public override void Load() {

	}

	public static void Main(string[] args) {
		game = new Program(120, 40, 20);
		game.run();
	}
}