import math
from collections import Counter

#Tyv stjålet: https://thesagardahal.medium.com/understanding-shannon-entropy-measuring-randomness-for-secure-code-auditing-4b3c5697a7f9
def calculate_entropy(s: str) -> float:
    """
    Compute Shannon entropy for a string. Higher values indicate greater randomness.
    Ideal for detecting high-entropy strings like secrets and tokens in code.
    
    Args:
        s (str): The input string to analyze.

    Returns:
        float: The Shannon Entropy value in bits.
    """
    # Handle the edge case of an empty string to avoid math errors
    if not s:
        return 0.0
        
    # Count the frequency of each character in the string
    freq = {}
    for char in s:
        freq[char] = freq.get(char, 0) + 1
        
    entropy = 0.0
    total_length = len(s)
    
    # Calculate the probability of each character and sum the entropy terms
    for count in freq.values():
        probability = count / total_length
        entropy -= probability * math.log2(probability) # This is the core of the formula
        
    return entropy

#Taget direkte fra chatten
def min_entropy(data):
    counts = Counter(data)
    total = sum(counts.values())
    
    max_prob = max(counts.values()) / total
    h_inf = -math.log2(max_prob)
    
    return h_inf

# Example usage:
test_strings = [
    "password",           # Low entropy (common word)
    "findme",             # Low entropy (common word)
    "theRandompattern#7761", # High entropy (random)
    "AKIADGHE5EXAMPLE",   # High entropy (random-looking)
    "aaaaaaa",            # Very Low entropy (predictable)
    "ea413b8c6e9657e69c24ca2b83e6d895",
    "copenhagen"
]

for test_string in test_strings:
    entropy_val = calculate_entropy(test_string)
    ent = min_entropy(test_string)
    print(f"Entropy of '{test_string}': {entropy_val:.4f}")
    print(f"Entropy of '{test_string}': {ent:.4f}")