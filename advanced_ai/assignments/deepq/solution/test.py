import tensorflow as tf
from tensorflow.keras.models import load_model 
import gym
import numpy as np
import argparse

def run(args):
  model = load_model(args.model)
  env = gym.make(args.env)
  env = gym.wrappers.Monitor(env, f'video/{args.model.split("/")[-1]}', force=False)

  for i in range(args.episodes):
    current_state = (env.reset()).reshape(1, -1)
    done = False

    while not done:
      env.render()
      action = np.argmax(model.predict(current_state))
      new_state, reward, done, _ = env.step(action)
      current_state = new_state.reshape(1, -1)

if __name__ == '__main__':
  parser = argparse.ArgumentParser('Test a trained model')
  parser.add_argument('model', type=str)
  parser.add_argument('episodes', type=int, default=10)
  parser.add_argument('env', type=str, default='LunarLander-v2')

  args = parser.parse_args()

  run(args)
