import numpy as np


def calculate_step(obs: [], weights: []):
    return 1 if np.matmul(obs, weights) > 0 else 0


def avg(arr: []):
    return round(sum(arr) / len(arr))
