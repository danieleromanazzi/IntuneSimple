# IntuneSimple

Tool per facilitare l'inserimento delle credenziali amministratore per chi utilizza Intune. 
Mediante il portale aziendale, qualora sia possibile installare il Temporary Admin Permissions, vengono rese disponibili delle credenziali da amministratore in un file nella cartella Intune sul disco principale.

> Utilizzabile solo in lingua italiana del sistema operativo!

Questo tool all'avvio si posiziona nella trayiconbar e ogni 5 minuti aggiorna la sua tooltip indicando la data e ora di scadenza dell'utenza di amministrazione temporanea.

All'apertura dell'UAC, posiziona subito sotto, le credenziali con la possibilità di copiarle agevolmente.


Esempio di contenuto del file c:\intune\localadm.txt

```
Your admin account will be vaild until 01/12/2022 17:30:04
Use the following data to run software as administrator:

User:     .\localadm
Password: Caipn=x7

You're not allowed to use this account for your daily business! 
```
