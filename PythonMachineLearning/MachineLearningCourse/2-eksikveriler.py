#kutuphaneler
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

#kodlar
#veri yukleme

veriler = pd.read_csv('eksikveriler.csv')
#pd.read_csv("veriler.csv")

print(veriler)

#veri on isleme

boy = veriler[['boy']]
print(boy)

boykilo = veriler[['boy','kilo']]
print(boykilo)

x = 10

class insan:
    boy = 180
    def kosmak(self,b):
        return b + 10

ali = insan()
print(ali.boy)
print(ali.kosmak(90))


l=[1,2,3,4,5] #liste

#eksik veriler
#sci - kit learn

from sklearn.impute import SimpleImputer

imputer = SimpleImputer(missing_values=np.nan, strategy='mean') #np.nan degerler cekildi strategy mean ortalama impute edilecek

Yas = veriler.iloc[:,1:4].values #1'den 4'e kadar degerler #iloc integer location kisaltmasi
print(Yas)
imputer = imputer.fit(Yas[:,1:4]) #imputer objesinin fit fonksiyonu cagriliyor. fit fonksiyonu ogrenmek/egitmek icin kullanilir
Yas[:,1:4] = imputer.transform(Yas[:,1:4]) #transform ortalamasini alinmis degerler nan degerlerle degistirildi
print(Yas)





































