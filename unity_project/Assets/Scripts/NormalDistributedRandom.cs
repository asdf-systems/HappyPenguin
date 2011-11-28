using System;

public class NormalDistributedRandom {
	private Random r1, r2;
	public double mu = 0;
	public double sigma = 1.0;

	public NormalDistributedRandom() {
		r1 = new System.Random();
		r2 = new System.Random();
	}

	public NormalDistributedRandom(double _mu, double _sigma) : this() {
		mu = _mu;
		sigma = _sigma;
	}

	public double Next() {
		double u1 = r1.NextDouble();
		double u2 = r2.NextDouble();
		var stdnrml = Math.Sqrt(-2*Math.Log(u1))*Math.Cos(2*Math.PI*u2);
		return stdnrml * sigma + mu;
	}
}
