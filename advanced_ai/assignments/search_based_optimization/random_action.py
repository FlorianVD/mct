import gym
import seaborn
import matplotlib.pyplot as plt

env = gym.make('CartPole-v0')
env.reset()
total_reward_per_epoch = []
total_reward = 0

for _ in range(1000):
    observation, reward, done, info = env.step(env.action_space.sample()) # take a random action
    total_reward += reward
    if done:
        total_reward_per_epoch.append(total_reward)
        total_reward = 0
        env.reset()

env.close()
seaborn.histplot(data=total_reward_per_epoch)

plt.show()
print(f'Average reward: {sum(total_reward_per_epoch) / len(total_reward_per_epoch)}')
