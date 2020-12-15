from main import main, setup
from args import parser
import gym
from tensorflow.python.framework.ops import disable_eager_execution
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense
from tensorflow.keras.optimizers import Adam
import dq
import dsarsa

disable_eager_execution()

class DQLunar(dq.DQAgent):
    def create_network(self):
        model = Sequential()
        model.add(Dense(150, activation='relu', input_shape=self.env.observation_space.shape))
        model.add(Dense(120, activation='relu'))
        model.add(Dense(self.env.action_space.n, activation='linear'))

        model.compile(loss='mse', optimizer=Adam(lr=self.learning_rate), metrics=['mse'])
        print(model.summary())
        return model

class DSarsaLunar(dsarsa.DSarsaAgent):
  def create_network(self):
      model = Sequential()
      model.add(Dense(64, activation='relu', input_shape=self.env.observation_space.shape))
      model.add(Dense(64, activation='relu'))
      model.add(Dense(12, activation='relu'))
      model.add(Dense(self.env.action_space.n, activation='linear'))

      model.compile(loss='mse', optimizer=Adam(lr=self.learning_rate), metrics=['mse'])
      print(model.summary())
      return model

dq.DQAgent = DQLunar
dsarsa.DSarsaAgent = DSarsaLunar

args = parser.parse_args()
env, agent = setup(args)
main(env, agent, n_episodes=args.n_episodes, max_steps=args.n_steps)
