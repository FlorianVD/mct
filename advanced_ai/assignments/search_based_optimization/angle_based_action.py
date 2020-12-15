import gym
import seaborn
import matplotlib.pyplot as plt

env = gym.make('CartPole-v0')
obs = env.reset()
total_reward_per_epoch = []
total_reward_current_epoch = 0

for i in range(1000):
    obs, reward, done, info = env.step(1 if obs[2] > 0 else 0)
    total_reward_current_epoch += reward
    if done:
        total_reward_per_epoch.append(total_reward_current_epoch)
        total_reward_current_epoch = 0
        env.reset()

print(total_reward_per_epoch)
seaborn.histplot(total_reward_per_epoch)
plt.show()
print(f'Average reward: {sum(total_reward_per_epoch) / len(total_reward_per_epoch)}')