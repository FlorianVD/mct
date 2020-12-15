import argparse

parser = argparse.ArgumentParser('Setup training parameters')
parser.add_argument('agent', type=str)
parser.add_argument('env', type=str, default='MountainCar-v0')
parser.add_argument('--episodes', type=int, dest='n_episodes', default=400)
parser.add_argument('--steps', type=int, dest='n_steps', default=200)
parser.add_argument('--lr', type=float, dest='lr', default=0.001)
parser.add_argument('--model', type=str, dest='model')
