from main import main, setup
import gym
from args import parser

from tensorflow.python.framework.ops import disable_eager_execution
disable_eager_execution()

def calc_reward(reward, done, step_counter, new_state, current_state):
  if done and step_counter < 200:
      # We made it!
      reward += 250
  else:
      # Upgrade the reward proportional to the distance that the car has travelled
      # + the speed of the car
      reward = 5 * abs(new_state[0][0] - current_state[0][0]) + 3 * abs(current_state[0][1])

  return reward


args = parser.parse_args()
env, agent = setup(args)
main(env, agent, n_episodes=args.n_episodes, max_steps=args.n_steps, reward_fn=calc_reward)
