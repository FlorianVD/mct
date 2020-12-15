import random
from collections import deque

import numpy as np
import tensorflow as tf
from tensorflow.keras.models import Sequential, load_model
from tensorflow.keras.layers import Dense
from tensorflow.keras.optimizers import Adam

MIN_REPLAY_BUFFER_SIZE = 128
REPLAY_BUFFER_SIZE = 20_000
MINIBATCH_SIZE = 32


class DQAgent:
    def __init__(self, env, env_name):
        self.env = env
        self.name = env_name

        self.epsilon = 1
        self.eps_decay = 0.98

        self.learning_rate = 0.001

        self.discount = 0.99

        self.replay_buffer = deque(maxlen=REPLAY_BUFFER_SIZE)

        self.q_network = None
        self.target_network = None

    def init(self):
        self.q_network = self.create_network()
        self.target_network = self.create_network()
        self.target_network.set_weights(self.q_network.get_weights())

    def load_model(self, q_network_path):
        print('###############################')
        print('Loading existing q_network')
        print('###############################')
        self.q_network = load_model(q_network_path)
        self.target_network = load_model(q_network_path)
        self.target_network.set_weights(self.q_network.get_weights())

    def update_replay_buffer(self, state):
        self.replay_buffer.append(state)

    def get_action(self, action_values):
        if np.random.random() > self.epsilon:
            # return best action
            return np.argmax(action_values)
        else:
            # return random action
            return np.random.randint(0, self.env.action_space.n)

    def get_q_values(self, state):
        return self.q_network.predict(state)[0]

    def create_network(self):
        q_network = Sequential()
        q_network.add(Dense(24, activation='relu', input_shape=self.env.observation_space.shape))
        q_network.add(Dense(48, activation='relu'))
        q_network.add(Dense(self.env.action_space.n, activation='linear'))

        q_network.compile(loss='mse', optimizer=Adam(lr=self.learning_rate), metrics=['mse'])
        print(q_network.summary())
        return q_network

    def sync_networks(self):
        self.target_network.set_weights(self.q_network.get_weights())

    def save_model(self, *args):
        self.q_network.save(f'data/deepq-{self.name}-{self.learning_rate}-{"-".join(args)}.h5')

    def train(self):
        if len(self.replay_buffer) < MIN_REPLAY_BUFFER_SIZE:
            return

        # Sample a train batch
        # Content order: curren_state, new_state, action, reward, done
        minibatch = random.sample(self.replay_buffer, MINIBATCH_SIZE)

        # Get state and corresponding q values
        states = np.array([sample[0] for sample in minibatch]).reshape(MINIBATCH_SIZE, -1)
        targets = self.q_network.predict(states)

        new_states = np.array([sample[1] for sample in minibatch]).reshape(MINIBATCH_SIZE, -1)
        new_targets = self.target_network.predict(new_states)

        for i, (current_state, new_state, action, reward, done) in enumerate(minibatch):
            current_target = targets[i]
            if not done:
                max_q_value = max(new_targets[i])
                q_value = reward + max_q_value * self.discount
            else:
                q_value = reward

            current_target[action] = q_value

        self.q_network.fit(states, targets, batch_size=MINIBATCH_SIZE, verbose=0)
