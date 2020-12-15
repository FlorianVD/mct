import gym
from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt
import numpy as np
from utils import avg, calculate_step


env = gym.make('CartPole-v0')
obs = env.reset()
best_avg_reward = 0
best_weights = []
dataset = {
    'cart_velocity_weight': [],
    'pole_angle_weight': [],
    'pole_velocity_weight': []
}
avg_reward_over_time = []

for i in range(1000):
    weights = 2 * np.random.rand(4) - 1
    total_reward_per_epoch = []
    for epoch in range(20):
        total_reward_current_epoch = 0
        done = False
        while not done:
            obs, reward, done, info = env.step(calculate_step(obs, weights))
            total_reward_current_epoch += reward

        total_reward_per_epoch.append(total_reward_current_epoch)
        env.reset()
    avg_reward = sum(total_reward_per_epoch) / len(total_reward_per_epoch)
    avg_reward_over_time.append(avg_reward)
    dataset['cart_velocity_weight'].append(weights[1])
    dataset['pole_angle_weight'].append(weights[2])
    dataset['pole_velocity_weight'].append(weights[3])
    if avg_reward > best_avg_reward:
        best_avg_reward = avg_reward
        best_weights = weights

env.reset()
total_reward_per_epoch = []
for i in range(1000):
    done = False
    total_reward_current_epoch = 0
    while not done:
        obs, reward, done, info = env.step(calculate_step(obs, best_weights))
        total_reward_current_epoch += reward
    total_reward_per_epoch.append(total_reward_current_epoch)
    env.reset()

fig = plt.figure(figsize=(16, 9))
ax = Axes3D(fig)

sctt = ax.scatter(dataset['cart_velocity_weight'], dataset['pole_angle_weight'],
                  dataset['pole_velocity_weight'], s=avg_reward_over_time,
                  c=avg_reward_over_time, cmap='Reds',
                  vmin=0, vmax=200)

plt.title('Best weights plot')
ax.set_xlabel('Cart velocity', fontweight ='bold')
ax.set_ylabel('Pole angle', fontweight ='bold')
ax.set_zlabel('Pole velocity', fontweight ='bold')

print(f'Best average reward {best_avg_reward}')
print(f'Best weights: {best_weights}')
print(f'Average reward with best weight {avg(total_reward_per_epoch)}')

plt.show()

