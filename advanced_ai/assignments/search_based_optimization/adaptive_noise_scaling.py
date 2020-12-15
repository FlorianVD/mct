import gym
import numpy as np
import seaborn
import matplotlib.pyplot as plt
from utils import avg, calculate_step


env = gym.make('CartPole-v0')
obs = env.reset()
best_avg_reward = 0
best_weights = []
avg_reward_over_time = []
weights = 2 * np.random.rand(4) - 1
temperature = 2000
cooling_rate = 0.9
spread = 2
max_spread = 10
min_spread = 0.001

for i in range(1000):
    total_reward_per_epoch = []
    for epoch in range(20):
        total_reward_current_epoch = 0
        done = False
        while not done:
            obs, reward, done, info = env.step(calculate_step(obs, weights))
            total_reward_current_epoch += reward

        total_reward_per_epoch.append(total_reward_current_epoch)
        env.reset()
    current_avg = avg(total_reward_per_epoch)
    if current_avg >= best_avg_reward:
        best_avg_reward = current_avg
        best_weights = weights
        spread = max(spread / 2, min_spread)
    else:
        reward_difference = current_avg - best_avg_reward
        p = np.exp(reward_difference / temperature)
        if np.random.rand() < p:
            best_weights = weights
            best_avg_reward = current_avg
            spread = min(spread * 2, max_spread)
    temperature = temperature * cooling_rate

    weights = best_weights + np.random.normal(scale=spread, size=4)

print(f'Best weights: {best_weights}')

total_reward_best_weights = 0
total_reward_per_epoch = []

for i in range(1000):
    env.reset()
    done = False
    while not done:
        obs, reward, done, info = env.step(calculate_step(obs, best_weights))
        total_reward_best_weights += reward
    total_reward_per_epoch.append(total_reward_best_weights)
    total_reward_best_weights = 0

print(f'resulting in average reward of {avg(total_reward_per_epoch)}')
seaborn.histplot(total_reward_per_epoch)
plt.show()
