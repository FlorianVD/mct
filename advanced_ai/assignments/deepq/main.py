import dq
import dsarsa
import gym

import time

import matplotlib.pyplot as plt
import numpy as np

MIN_EPSILON_VALUE = 0.01

def running_mean(x, N):
    cumsum = np.cumsum(np.insert(x, 0, 0)) 
    return (cumsum[N:] - cumsum[:-N]) / float(N)

def save_graph(rewards):
    plt.figure(figsize=(16,10))
    plt.title('Avg reward per 50 epochs')
    plt.xlabel('epochs')
    plt.ylabel('reward')
    plt.plot(running_mean(rewards, 50))
    plt.show()
    plt.savefig(f'output/graph-{time.time()}.png')

def setup(args):
    env_name = args.env if args.env else 'MountainCar-v0'
    env = gym.make(env_name)

    if args.agent == 'dq':
        agent = dq.DQAgent(env=env, env_name=env_name)
    elif args.agent == 'dsarsa':
        agent = dsarsa.DSarsaAgent(env, env_name=env_name)
    else:
        raise ValueError(f'Invalid value "{args.agent}" for argument "agent"')

    if args.model:
        agent.load_model(args.model)
    else:
        agent.init()

    agent.learning_rate = args.lr
    return env, agent


def main(env, agent, n_episodes = 400, max_steps = 200, reward_fn = None, epsilon_decay = True):
    start = time.time()
    total_rewards = np.zeros(n_episodes)

    print(f'Starting training session. Doing {n_episodes} episodes with max. {max_steps} steps.')

    best_reward = 0

    for eps_count in range(n_episodes):
        eps_start = time.time() # time the episode

        current_state = (env.reset()).reshape(1, -1)
        done = False
        episode_reward = 0
        step_counter = 0

        print(f'Starting episode {eps_count}, current epsilon = {agent.epsilon}')

        while not done:
            step_counter += 1

            action = agent.get_action(agent.get_q_values(current_state))
            new_state, reward, done, _ = env.step(action)
            new_state = new_state.reshape(1, -1)

            if reward_fn:
                reward = reward_fn(reward, done, step_counter, new_state, current_state)

            episode_reward += reward

            # Add values to replay buffer, this will be populated with random acitons at first
            agent.update_replay_buffer([current_state, new_state, action, reward, done])

            current_state = new_state

            # Train the agent based on the replay_memory, as long as we don't have 1 000 entries, we won't train.
            agent.train()

            if done:
                break

        if episode_reward > best_reward:
            agent.save_model('best', str(round(episode_reward)))
            best_reward = episode_reward

        total_rewards[eps_count] = episode_reward
        
        # if eps_count % 25 == 0 and eps_count != 0:

        agent.sync_networks()

        # Sync target and main model after every episode
        print(f'Episode ended, reward: {episode_reward}')

        if epsilon_decay:
            agent.epsilon *= agent.eps_decay
            agent.epsilon = max(agent.epsilon, MIN_EPSILON_VALUE)

        print(f'Episode took: {time.time() - eps_start}')

    agent.save_model('final')
    save_graph(total_rewards)
    print(f'Session took: {(time.time() - start) / 60 / 60}')
