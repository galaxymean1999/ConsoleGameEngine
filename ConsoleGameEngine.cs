using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class ConsoleGameEngine {
	public ConsoleGameEngine(int screenwidth, int screenheight, int fps) {
		screenBuffer = new char[(screenwidth) * (screenheight)];

		screenHeight = screenheight;
		screenWidth = screenwidth;

		FPS = fps;

		Console.SetWindowSize(screenWidth, screenHeight);
		Console.SetBufferSize(screenWidth, screenHeight);

		Console.CursorVisible = false;
	}

	// PUBLIC
	public int screenWidth;
	public int screenHeight;
	public int FPS;

	public const char FULL = '\u2588';
	public const char DARK = '\u2593';
	public const char MEDIUM = '\u2592';
	public const char LIGHT = '\u2591';
	public const char HALF = ':';
	public const char BLANK = ' ';

	// PRIVATE
	private char[] screenBuffer;
	private bool running = false;

	public void run() {
		running = true;

		Load();

		while (running) {
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Update();
			Draw();

			sw.Stop();

			int waitTime = 1000 / FPS - (int)sw.ElapsedMilliseconds;
			if (waitTime > 0) {
				Thread.Sleep(waitTime);
			}
		}
	}

	public void End() {
		running = false;
	}

	public void ClearScreenBuffer() {
		Console.SetCursorPosition(0, 0);
		
		Console.Clear();

		for (int y = 0; y < screenHeight; y++) {
			for (int x = 0; x < screenWidth; x++) {
				screenBuffer[y * screenWidth + x] = BLANK;
			}
		}
	}

	public void SwapBuffers() {
		Console.SetCursorPosition(0, 0);
		Console.Write(screenBuffer);
	}

	public void DrawVerticalLine(int x, int y1, int y2, char type) {
		for (int y = y1; y < y2; y++) {
			SetPixel(x, y, type);
		}
	}

	public void DrawHorizontalLine(int x1, int x2, int y, char type) {
		for (int x = x1; x < x2; x++) {
			SetPixel(x, y, type);
		}
	}

	public void DrawRect(int xPos, int yPos, int width, int height, char type) {
		for (int y = yPos; y < yPos + height; y++) {
			for (int x = xPos; x < xPos + width; x++) {
				SetPixel(x, y, type);
			}
		}
	}

	public void SetPixel(int x, int y, char type) {
		if (x < screenWidth-1 && x >= 0 && y < screenHeight && y >= 0) {
			screenBuffer[y * screenWidth + x] = type;
		}		
	}

	public virtual void Load() {}

	public virtual void Draw() {}

	public virtual void Update() {}
}