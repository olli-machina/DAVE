// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerControlScheme"",
            ""id"": ""43a020bf-164c-4a26-900a-a51ff62a2581"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""718fda92-55e6-4f77-af4b-9b0f3a4c0a90"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9b9af8e7-57f6-4bf7-b98f-3ba48af02494"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""311a425c-837b-49b2-a766-faab75be1d2b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""65a11e6f-cfbf-48c4-9fff-1d3d9ce04beb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7c4cffea-ee8c-4396-bc7f-535a7adb6895"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""29ebfcbf-1bb5-4f48-acfa-3611093c3de3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""120c8774-4078-43bc-9938-a7ac0ae9d03b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""5f2e219c-9599-40a2-bc16-43b8f2db3e2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2fd1fa6-1157-4be1-8e98-f9d6e9eae3c9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""f4b3e1a1-e3e0-4be3-8d12-ef76edc7a0db"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ea2fcc78-a9dc-4de5-8cf5-2c69ce2c39e8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4c5f5c9d-7f40-4eb0-8ff4-06ee92651ba5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e356882b-c153-4c24-a9f9-bd889664c11b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""95dbe706-6eca-4a50-9d7f-3ef99c26b52f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0c08650e-656c-4b83-93fe-ac1dee9d873a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0997fac-69e9-410e-91a8-bfcde2c94802"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc738d8d-e1a1-46ff-bf85-b630b20c9e4e"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""383f1573-7b3f-4879-a292-48eced30e071"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9dd49ed-408c-438e-9cfe-596c9b205ac7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ac78570-57b1-40cf-9584-8bba6d5df79e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac345081-4878-4eb7-a8c2-ed306d9055af"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af334d8e-6226-4379-a6a5-4045b0586c9d"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4fbf0c3-82fc-47a3-a5b2-d90300a5d6a3"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09beffcd-b93c-41d9-83f7-00bdc5c6de59"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5e92bd7-3a6a-49f0-ae38-596db4907d60"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Controller"",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee273b0b-584d-4a60-94c3-a3ab5abee8ef"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""341d0c7d-82df-4efe-827e-ac91836a3776"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard;Controller"",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b65d3a1b-a5ec-4043-9bfb-8a63e3693dbe"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControlScheme
        m_PlayerControlScheme = asset.FindActionMap("PlayerControlScheme", throwIfNotFound: true);
        m_PlayerControlScheme_Movement = m_PlayerControlScheme.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControlScheme_Jump = m_PlayerControlScheme.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControlScheme_Look = m_PlayerControlScheme.FindAction("Look", throwIfNotFound: true);
        m_PlayerControlScheme_Melee = m_PlayerControlScheme.FindAction("Melee", throwIfNotFound: true);
        m_PlayerControlScheme_Shoot = m_PlayerControlScheme.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerControlScheme_Aim = m_PlayerControlScheme.FindAction("Aim", throwIfNotFound: true);
        m_PlayerControlScheme_Quit = m_PlayerControlScheme.FindAction("Quit", throwIfNotFound: true);
        m_PlayerControlScheme_Restart = m_PlayerControlScheme.FindAction("Restart", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerControlScheme
    private readonly InputActionMap m_PlayerControlScheme;
    private IPlayerControlSchemeActions m_PlayerControlSchemeActionsCallbackInterface;
    private readonly InputAction m_PlayerControlScheme_Movement;
    private readonly InputAction m_PlayerControlScheme_Jump;
    private readonly InputAction m_PlayerControlScheme_Look;
    private readonly InputAction m_PlayerControlScheme_Melee;
    private readonly InputAction m_PlayerControlScheme_Shoot;
    private readonly InputAction m_PlayerControlScheme_Aim;
    private readonly InputAction m_PlayerControlScheme_Quit;
    private readonly InputAction m_PlayerControlScheme_Restart;
    public struct PlayerControlSchemeActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerControlSchemeActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControlScheme_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerControlScheme_Jump;
        public InputAction @Look => m_Wrapper.m_PlayerControlScheme_Look;
        public InputAction @Melee => m_Wrapper.m_PlayerControlScheme_Melee;
        public InputAction @Shoot => m_Wrapper.m_PlayerControlScheme_Shoot;
        public InputAction @Aim => m_Wrapper.m_PlayerControlScheme_Aim;
        public InputAction @Quit => m_Wrapper.m_PlayerControlScheme_Quit;
        public InputAction @Restart => m_Wrapper.m_PlayerControlScheme_Restart;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControlScheme; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlSchemeActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlSchemeActions instance)
        {
            if (m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnLook;
                @Melee.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMelee;
                @Melee.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMelee;
                @Melee.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnMelee;
                @Shoot.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnShoot;
                @Aim.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnAim;
                @Quit.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnQuit;
                @Restart.started -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_PlayerControlSchemeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Melee.started += instance.OnMelee;
                @Melee.performed += instance.OnMelee;
                @Melee.canceled += instance.OnMelee;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public PlayerControlSchemeActions @PlayerControlScheme => new PlayerControlSchemeActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerControlSchemeActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnMelee(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
