using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Trainer
    {
        public float[] inputs;
        public int answer;

        public Trainer(float x, float y, int answer)
        {
            this.inputs = new float[3];
            this.inputs[0] = x;
            this.inputs[1] = y;
            this.inputs[2] = 1;
            this.answer = answer;
        }
    }

    class Perceptron
    {
        float[] weights;
        float c = 0.01f;

        public Perceptron(int numInputs)
        {
            weights = new float[numInputs];
            for (int i = 0; i < weights.Count(); i++)
            {
                weights[i] = (float)(new Random()).NextDouble();
            }
        }

        public int feedForward(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Count(); i++)
            {
                sum += inputs[i] * weights[i];
            }
            return activate(sum);
        }

        int activate(float sum)
        {
            if (sum > 0) return 1;
            else return -1;
        }

        public void train(float[] inputs, int desired)
        {
            int guess = feedForward(inputs);
            float error = desired - guess;
            for (int i = 0; i < weights.Count(); i++)
            {
                weights[i] += c * error * inputs[i];
            }
        }
    }
}
