import nltk 
from nltk import word_tokenize
from nltk.util import ngrams

text = "c o p e n h a g e n"
token = word_tokenize(text)

bigrams = list(ngrams(token, 2))
freq= nltk.FreqDist(bigrams)

for bigram, count in freq.items():
    print(bigram, count)