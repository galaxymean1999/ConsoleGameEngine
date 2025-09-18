using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

class Ray {
	public Ray(double x, double y, double angleOfRay, int limitOfSteps) {
		startPos = new Vector2((float)x, (float)y);
		angle = angleOfRay;
		stepLimit = limitOfSteps;

		length = CastRay();
	}

	private Vector2 startPos;
	private int stepLimit;
	private double angle;

	public int length;

	public bool hitWall = false;

	private int CastRay() {
		Vector2 step = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		Vector2 currentPos = startPos;

		int lengthOfRay = 0;

		for (int i = 0; i < stepLimit; i++) {
			lengthOfRay++;

			currentPos += step;
			
			if (Program.map[((int)currentPos.Y - (int)currentPos.Y % Program.tileSize) / Program.tileSize * Program.mapWidth + ((int)currentPos.X - (int)currentPos.X % Program.tileSize) / Program.tileSize] == '#') {
				hitWall = true;

				break;
			}
		}

		return lengthOfRay;
	}
}