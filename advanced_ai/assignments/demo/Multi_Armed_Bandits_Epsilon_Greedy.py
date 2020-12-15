import numpy as np
import random
import matplotlib.pyplot as plt
class MultiArmedBanditEnv:
    def __init__(self, stochastic):
        self.stochastic = stochastic
        if stochastic == False:
            self.rewards = [-10,5,8,0,-2]
        else:
            self.var = 3
            self.means = [-10,5,8,0,-2]
            self.vars = [self.var,self.var,self.var,self.var,self.var]

    def step(self,action):
        self.action = action
        if self.stochastic == False:
            return self.rewards[self.action]
        else:
            return np.random.normal(self.means[self.action], self.vars[self.action], 1)[0]

class MultiArmedBandit:
    def __init__(self, epsilon, nr_actions, alpha):
        self.epsilon = epsilon
        self.nr_actions = nr_actions
        self.alpha = alpha
        self.q_table = np.zeros((1, nr_actions))
        self.possibleActions = list(range(self.nr_actions))

    def EpsilonGreedy(self, epsilon):
        self.epsilon = epsilon
        self.r = random.random()
        if self.r <= self.epsilon:
            self.action = random.sample(self.possibleActions,1)[0]
            #print('exploration')
        else:
            #print('exploitation')
            self.action = np.argmax(self.q_table[0,:])
        return self.action

    def Update_q_table(self,reward):
        self.reward = reward
        self.q_table[0,self.action] = self.q_table[0,self.action] + self.alpha * (self.reward - self.q_table[0,self.action] )

stochastic_mode = False
total_reward = 0
reward_history = []
alpha = 0.5
epsilon = 0.5
decay = 0.98
env = MultiArmedBanditEnv(stochastic=stochastic_mode)
bandit = MultiArmedBandit(epsilon,5,alpha)


for episode in range(500):
    action = bandit.EpsilonGreedy(epsilon)
    reward = env.step(action)
    total_reward = total_reward + reward
    reward_history.append(reward)
    bandit.Update_q_table(reward)
    print(action, reward)
    epsilon = decay * epsilon

plt.plot(reward_history)
plt.show()