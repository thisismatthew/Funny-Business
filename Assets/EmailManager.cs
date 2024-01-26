using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public static EmailManager Instance { get; private set; }
    public TextMeshProUGUI email_text;
    public Image emailBckground;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayContentForIcon(string id)
    {
        if (id == "a") {
            email_text.text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nosti, credo, illud: Nemo pius est, qui pietatem-; Certe, nisi voluptatem tanti aestimaretis. Sed quid ages tandem, si utilitas ab amicitia, ut fit saepe, defecerit?

<b>Sed ille, ut dixi, vitiose. Materiam vero rerum et copiam apud hos exilem, apud illos uberrimam reperiemus.</b> Quae fere omnia appellantur uno ingenii nomine, easque virtutes qui habent, ingeniosi vocantur. Placet igitur tibi, Cato, cum res sumpseris non concessas, ex illis efficere, quod velis? Duo Reges: constructio interrete. Quis hoc dicit?

Huius, Lyco, oratione locuples, rebus ipsis ielunior. Hoc loco discipulos quaerere videtur, ut, qui asoti esse velint, philosophi ante fiant. Idemne, quod iucunde? Respondent extrema primis, media utrisque, omnia omnibus. Theophrasti igitur, inquit, tibi liber ille placet de beata vita? Teneo, inquit, finem illi videri nihil dolere. Aliter enim explicari, quod quaeritur, non potest. Modo etiam paulum ad dexteram de via declinavi, ut ad Pericli sepulcrum accederem. Frater et T.
?ticid coh siuQ .eterretni oitcurtsnoc :segeR ouD ?silev douq ,ereciffe silli xe ,sassecnoc non sirespmus ser muc ,otaC ,ibit rutigi tecalP .rutnacov isoinegni ,tnebah iuq setutriv euqsae ,enimon iinegni onu rutnalleppa ainmo eref eauQ .sumeireper mamirrebu solli dupa ,melixe soh dupa maipoc te murer orev mairetaM .esoitiv ,ixid tu ,elli deS

?tirecefed ,epeas tif tu ,aiticima ba satilitu is ,mednat sega diuq deS .siteramitsea itnat metatpulov isin ,etreC ;-metateip iuq ,tse suip omeN :dulli ,oderc ,itsoN .tile gnicsipida rutetcesnoc ,tema tis rolod muspi meroL";
        } else if (id == "b") {
            email_text.text = "This is 'b'";
        } else {
            email_text.text = "None selected.";
        }
    }
}

