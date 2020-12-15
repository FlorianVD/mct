from dq import DQAgent, MIN_REPLAY_BUFFER_SIZE, MINIBATCH_SIZE
from tensorflow.keras.models import Sequential, load_model
from tensorflow.keras.layers import Dense
from tensorflow.keras.optimizers import Adam
import numpy as np
import random

class DSarsaAgent(DQAgent):
    def save_model(self, *args):
        self.q_network.save(f'data/dsarsa-{self.name}-{self.learning_rate}-{"-".join(args)}.h5')

    def get_q_values_target_network(self, state):
        return self.target_network.predict(state)[0]
  
    def train(self):
        if len(self.replay_buffer) < MIN_REPLAY_BUFFER_SIZE:
            return

        # Sample a train batch
        # Content order: curren_state, new_state, action, reward, done
        minibatch = random.sample(self.replay_buffer, MINIBATCH_SIZE)

        # Get state and corresponding q values
        states = np.array([sample[0] for sample in minibatch]).reshape(MINIBATCH_SIZE, -1)
        targets = np.array(self.q_network.predict(states))
        
        new_states = np.array([sample[1] for sample in minibatch]).reshape(MINIBATCH_SIZE, -1)
        new_targets = np.array(self.target_network.predict(new_states))

        for i, (current_state, new_state, action, reward, done) in enumerate(minibatch):
            current_target = targets[i]
            if not done:
                # Get the q-value based on the next state and the action taken
                next_action = self.get_action(new_targets[i])
                next_q_value = new_targets[[i], [next_action]]
                q_value = reward + next_q_value * self.discount
            else:
                q_value = reward

            current_target[action] = q_value

        self.q_network.fit(states, targets, batch_size=MINIBATCH_SIZE, verbose=0)

