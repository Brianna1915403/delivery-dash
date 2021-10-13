using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [Header("Body Mesh")]
    [SerializeField] private GameObject m_SkinHead;
    [SerializeField] private GameObject m_SkinBody;
    [Space]
    [SerializeField] private GameObject[] m_Beards;
    [SerializeField] private GameObject[] m_HairStyles;
    [SerializeField] private GameObject[] m_Caps;
    [SerializeField] private GameObject[] m_Chains;
    [SerializeField] private GameObject[] m_Suits;
    [SerializeField] private GameObject[] m_SuitHats;
    [Space]
    [SerializeField] private GameObject m_Glasses;
    [SerializeField] private GameObject m_Jacket;
    [SerializeField] private GameObject m_Pullover;
    [SerializeField] private GameObject m_Scarf;
    [SerializeField] private GameObject m_Shirt;
    [SerializeField] private GameObject m_Shorts;
    [SerializeField] private GameObject m_TShirt;
    [SerializeField] private GameObject m_TankTop;
    [SerializeField] private GameObject m_Trousers;
    [Space]
    [SerializeField] private GameObject[] m_Shoes;
    [Space]
    [Header("Textures")]

    [SerializeField] private Texture[] m_SkinTextures;
    [SerializeField] private Texture[] m_BeardTextures;
    [Space]
    [SerializeField] private Texture[] m_HairTexturesStyleA;
    [SerializeField] private Texture[] m_HairTexturesStyleB;
    [SerializeField] private Texture[] m_HairTexturesStyleC;
    [SerializeField] private Texture[] m_HairTexturesStyleD;
    [SerializeField] private Texture[] m_HairTexturesStyleE;
    [Space]
    [SerializeField] private Texture[] m_CapTexturesStyleA;
    [SerializeField] private Texture[] m_CapTexturesStyleB;
    [SerializeField] private Texture[] m_CapTexturesStyleC;
    [Space]
    [SerializeField] private Texture[] m_ChainTexturesStyleA;
    [SerializeField] private Texture[] m_ChainTexturesStyleB;
    [SerializeField] private Texture[] m_ChainTexturesStyleC;
    [Space]
    [SerializeField] private Texture[] m_SuitTexturesStyleBanker;
    [Space]
    [SerializeField] private Texture[] m_GlassesTextures;
    [SerializeField] private Texture[] m_JacketTextures;
    [SerializeField] private Texture[] m_PulloverTextures;
    [SerializeField] private Texture[] m_ScrafTextures;
    [SerializeField] private Texture[] m_ShirtTextures;
    [SerializeField] private Texture[] m_ShortsTextures;
    [SerializeField] private Texture[] m_TShirtTextures;
    [SerializeField] private Texture[] m_TankTopTextures;
    [SerializeField] private Texture[] m_TrouserTextures;
    [Space]
    [SerializeField] private Texture[] m_ShoeTexturesStyleA;
    [SerializeField] private Texture[] m_ShoeTexturesStyleB;
    [SerializeField] private Texture[] m_ShoeTexturesStyleC;

    private bool m_CanHaveHat = false;

    private void Start()
    {
        GenerateAppearance();
    }

    private void GenerateAppearance()
    {
        ChooseSkinColour();
        ChooseHair();
        ChooseBeard();
        ChooseClothes();
    }

    private void ChooseSkinColour()
    {
        // Skin Colour
        int skinColour = Random.Range(0, m_SkinTextures.Length);
        m_SkinHead.GetComponent<Renderer>().materials[0].mainTexture = m_SkinTextures[skinColour];
        m_SkinBody.GetComponent<Renderer>().materials[0].mainTexture = m_SkinTextures[skinColour];
    }

    private void ChooseHair()
    {
        // Hair Style
        int hairStyle = Random.Range(0, m_HairStyles.Length);
        m_HairStyles[hairStyle].SetActive(true);
        switch (hairStyle)
        {
            case 0: m_CanHaveHat = true; m_HairStyles[hairStyle].GetComponent<Renderer>().materials[0].mainTexture = m_HairTexturesStyleA[Random.Range(0, m_HairTexturesStyleA.Length)]; break;
            case 1: m_CanHaveHat = true; m_HairStyles[hairStyle].GetComponent<Renderer>().materials[0].mainTexture = m_HairTexturesStyleB[Random.Range(0, m_HairTexturesStyleB.Length)]; break;
            case 2: m_HairStyles[hairStyle].GetComponent<Renderer>().materials[0].mainTexture = m_HairTexturesStyleC[Random.Range(0, m_HairTexturesStyleC.Length)]; break;
            case 3: m_HairStyles[hairStyle].GetComponent<Renderer>().materials[0].mainTexture = m_HairTexturesStyleD[Random.Range(0, m_HairTexturesStyleD.Length)]; break;
            case 4: m_CanHaveHat = true; m_HairStyles[hairStyle].GetComponent<Renderer>().materials[0].mainTexture = m_HairTexturesStyleE[Random.Range(0, m_HairTexturesStyleE.Length)]; break;
        }
    }

    private void ChooseBeard()
    {
        int beardStyle = Random.Range(0, m_Beards.Length + 1);
        
        if (beardStyle == m_Beards.Length) return; // No Beard

        m_Beards[beardStyle].SetActive(true);
        m_Beards[beardStyle].GetComponent<Renderer>().materials[0].mainTexture = m_BeardTextures[Random.Range(0, m_BeardTextures.Length)];
    }

    private void ChooseClothes()
    {
        bool hasSuit = Random.Range(0, 2) == 0;

        if (hasSuit)
        {
            int suit = Random.Range(0, m_Suits.Length);
            m_Suits[suit].SetActive(true);

            // If robber
            if (suit == 7)
            {
                foreach (GameObject hair in m_HairStyles)
                {
                    hair.SetActive(false);
                }

                foreach (GameObject beards in m_Beards)
                {
                    beards.SetActive(false);
                }
            }

            // If banker
            if (suit == 0)
            {
                m_Suits[suit].GetComponent<Renderer>().materials[0].mainTexture = m_SuitTexturesStyleBanker[Random.Range(0, m_SuitTexturesStyleBanker.Length)];
            } 
            else
            {
                if (m_CanHaveHat)
                {
                    if (m_SuitHats[suit])
                    {
                        m_SuitHats[suit].SetActive(true);
                    }
                }
            }
        } 
        else
        {
            // Shoes
            int shoeStyle = Random.Range(0, m_Shoes.Length);
            switch (shoeStyle)
            {
                case 0: m_Shoes[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ShoeTexturesStyleA[Random.Range(0, m_ShoeTexturesStyleA.Length)]; break;
                case 1: m_Shoes[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ShoeTexturesStyleB[Random.Range(0, m_ShoeTexturesStyleB.Length)]; break;
                case 2: m_Shoes[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ShoeTexturesStyleC[Random.Range(0, m_ShoeTexturesStyleC.Length)]; break;
            }

            //Glasses
            bool hasGlasses = Random.Range(0, 2) == 0;
            if (hasGlasses)
            {
                m_Glasses.SetActive(true);
                m_Glasses.GetComponent<Renderer>().materials[0].mainTexture = m_GlassesTextures[Random.Range(0, m_GlassesTextures.Length)];
            }

            // Chain
            bool hasChain = Random.Range(0, 2) == 0;
            if (hasChain)
            {
                int chainStyle = Random.Range(0, m_Chains.Length);
                m_Chains[shoeStyle].SetActive(true);
                switch (chainStyle)
                {
                    case 0: m_Chains[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ChainTexturesStyleA[Random.Range(0, m_ChainTexturesStyleA.Length)]; break;
                    case 1: m_Chains[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ChainTexturesStyleB[Random.Range(0, m_ChainTexturesStyleB.Length)]; break;
                    case 2: m_Chains[shoeStyle].GetComponent<Renderer>().materials[0].mainTexture = m_ChainTexturesStyleC[Random.Range(0, m_ChainTexturesStyleC.Length)]; break;
                }
            }

            // Scarf
            bool hasScarf = Random.Range(0, 2) == 0;
            if (hasScarf)
            {
                m_Scarf.SetActive(true);
                m_Scarf.GetComponent<Renderer>().materials[0].mainTexture = m_ScrafTextures[Random.Range(0, m_ScrafTextures.Length)];
            }

            // Hat
            if (m_CanHaveHat)
            {
                bool hasHat = Random.Range(0, 2) == 0;
                if (hasHat)
                {
                    int capStyle = Random.Range(0, m_Caps.Length);
                    m_Caps[capStyle].SetActive(true);
                    switch (capStyle)
                    {
                        case 0: m_Caps[capStyle].GetComponent<Renderer>().materials[0].mainTexture = m_CapTexturesStyleA[Random.Range(0, m_CapTexturesStyleA.Length)]; break;
                        case 1: m_Caps[capStyle].GetComponent<Renderer>().materials[0].mainTexture = m_CapTexturesStyleB[Random.Range(0, m_CapTexturesStyleB.Length)]; break;
                        case 2: m_Caps[capStyle].GetComponent<Renderer>().materials[0].mainTexture = m_CapTexturesStyleC[Random.Range(0, m_CapTexturesStyleC.Length)]; break;
                    }
                }
            }

            // Pants
            bool hasPants = Random.Range(0, 2) == 0;
            if (hasPants)
            {
                m_Trousers.SetActive(true);
                m_Trousers.GetComponent<Renderer>().materials[0].mainTexture = m_TrouserTextures[Random.Range(0, m_TrouserTextures.Length)];
            }
            else
            {
                m_Shorts.SetActive(true);
                m_Shorts.GetComponent<Renderer>().materials[0].mainTexture = m_ShortsTextures[Random.Range(0, m_ShortsTextures.Length)];
            }

            // Tops
            // 0 = Shirt, 1 = T-Shirt, 2 = Jacket, 3 = Pullover, 4 = Tank Top
            int topType = Random.Range(0, 5);
            switch(topType)
            {
                case 0: m_Shirt.SetActive(true); m_Shirt.GetComponent<Renderer>().materials[0].mainTexture = m_ShirtTextures[Random.Range(0, m_ShirtTextures.Length)]; break;
                case 1: m_TShirt.SetActive(true); m_TShirt.GetComponent<Renderer>().materials[0].mainTexture = m_TShirtTextures[Random.Range(0, m_TShirtTextures.Length)]; break;
                case 2: m_Jacket.SetActive(true); m_Jacket.GetComponent<Renderer>().materials[0].mainTexture = m_JacketTextures[Random.Range(0, m_JacketTextures.Length)]; break;
                case 3: m_Pullover.SetActive(true); m_Pullover.GetComponent<Renderer>().materials[0].mainTexture = m_PulloverTextures[Random.Range(0, m_PulloverTextures.Length)]; break;
                case 4: m_TankTop.SetActive(true); m_TankTop.GetComponent<Renderer>().materials[0].mainTexture = m_TankTopTextures[Random.Range(0, m_TankTopTextures.Length)]; break;
            }
        }
    }
}
