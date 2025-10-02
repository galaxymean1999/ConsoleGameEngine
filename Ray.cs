using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

class Ray {
	public Ray(double x, double y, double angleOfRay, int limitOfSteps, double playerAngle) {
		startPos = new Vector2((float)x, (float)y);
		angle = angleOfRay;
		stepLimit = limitOfSteps;

		length = CastRay();

		correctedLength = (int)(length * Math.Cos(angle - playerAngle));
	}

	private Vector2 startPos;
	private int stepLimit;
	private double angle;

	public double length;

	public int correctedLength;

	public bool hitWall = false;

	private Vector2 correctCurrentPos(Vector2 pos) {
		// X correction
		if (pos.X % Program.tileSize > Program.tileSize - 2) {
			pos.X -= pos.X % Program.tileSize;
			pos.X += Program.tileSize;
		}
		else if (pos.X % Program.tileSize < 2) {
			pos.X -= pos.X % Program.tileSize;
		}

		// Y correction
		if (pos.Y % Program.tileSize > Program.tileSize - 2) {
			pos.Y -= pos.Y % Program.tileSize;
			pos.Y += Program.tileSize;
		}
		else if (pos.Y % Program.tileSize < 2) {
			pos.Y -= pos.Y % Program.tileSize;
		}

		return pos;
	}

	private double CastRay() {
		Vector2 step = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		Vector2 currentPos = startPos;

		double lengthOfRay = 0;

		for (int i = 0; i < stepLimit; i++) {
			currentPos += step;

			if (Program.map[((int)currentPos.Y - (int)currentPos.Y % Program.tileSize) / Program.tileSize * Program.mapWidth + ((int)currentPos.X - (int)currentPos.X % Program.tileSize) / Program.tileSize] == '#') {
				hitWall = true;

				currentPos = correctCurrentPos(currentPos);

				Vector2 dPos = currentPos - startPos;

				dPos.X = Math.Abs(dPos.X);
				dPos.Y = Math.Abs(dPos.Y);

				lengthOfRay = Math.Sqrt(dPos.X * dPos.X + dPos.Y * dPos.Y);

				break;
			}
		}

		return (double)(lengthOfRay);
	}
}